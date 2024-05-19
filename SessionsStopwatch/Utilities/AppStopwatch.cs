using SessionsStopwatch.ViewModels;
using SessionsStopwatch.Windows;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Media;
using System.Windows.Forms;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace SessionsStopwatch.Utilities
{
    static class AppStopwatch {
        private const string PathToRemindersXML = @"reminders.xml";
        private static readonly XmlSerializer _remindersSerializer = new(typeof(ObservableCollection<Reminder>));
        private static readonly DispatcherTimer _timer;

        public static ObservableCollection<Reminder> Reminders { get; }

        public static TimeSpan TimeElapsed { get; private set; }
        public static event Action? TimeElapsedChanged;

        static AppStopwatch() {
            Reminders = DeserializeReminders();
            Reminders.CollectionChanged += HandleRemindersChanged;

            _timer = new() {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Start();

            _timer.Tick += (object? sender, EventArgs e) => {
                TimeElapsed += TimeSpan.FromSeconds(1);
                OnTimeElapsedChanged();
            };
        }

        private static void HandleRemindersChanged(object? sender, NotifyCollectionChangedEventArgs e) {
            SerializeReminders();
            if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null) {
                foreach (var newReminder in e.NewItems) ((Reminder)newReminder).PropertyChanged += SingleReminderChanged;
            } else if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems != null) {
                foreach (var oldReminder in e.OldItems) ((Reminder)oldReminder).PropertyChanged -= SingleReminderChanged;
            }
        }

        private static void SingleReminderChanged(object? sender, PropertyChangedEventArgs e) {
            SerializeReminders();
        }

        public static void SerializeReminders() {
            using StreamWriter writer = new(PathToRemindersXML);
            _remindersSerializer.Serialize(writer, Reminders);
        }

        private static ObservableCollection<Reminder> DeserializeReminders() {
            if (!File.Exists(PathToRemindersXML)) return [];

            using StreamReader reader = new(PathToRemindersXML);
            ObservableCollection<Reminder>? deser = _remindersSerializer.Deserialize(reader) as ObservableCollection<Reminder>;
            if (deser != null) foreach (var reminder in deser) reminder.PropertyChanged += SingleReminderChanged; 

            return deser ?? [];
        }

        private static void OnTimeElapsedChanged() {
            TimeElapsedChanged?.Invoke();

            foreach (var reminder in Reminders) {
                if (reminder.Time == TimeElapsed) Remind(reminder.Time);
                else if (reminder.Behavior == ReminderBehavior.Repeat) {
                    double elapsedSeconds = TimeElapsed.TotalSeconds;
                    double reminderSeconds = reminder.Time.TotalSeconds;

                    if (elapsedSeconds > 0 && elapsedSeconds % reminderSeconds == 0)
                        Remind(reminder.Time, (int)(elapsedSeconds / reminderSeconds));
                }
            }
        }

        private static void Remind(TimeSpan time, int times = 1) {
            string text = times > 1 ? $"Hey, another {time} has passed ({times})" : $"Hey, {time} has passed";

            ReminderWindow remWin = new();
            remWin.DataContext = new ReminderVM(text, remWin);
            remWin.Show();
            SystemSounds.Beep.Play();
        }

        public static void Stop() {
            _timer.Stop();
        }

        public static void Restart() {
            _timer.Start();
            TimeElapsed = TimeSpan.Zero;
            OnTimeElapsedChanged();
        }
    }
}
