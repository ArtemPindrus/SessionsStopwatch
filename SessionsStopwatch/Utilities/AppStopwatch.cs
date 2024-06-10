using SessionsStopwatch.ViewModels;
using SessionsStopwatch.Windows;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace SessionsStopwatch.Utilities
{
    static class AppStopwatch {
        private const string PathToRemindersXML = @"reminders.xml";
        private static readonly XmlSerializer _remindersSerializer = new(typeof(ObservableCollection<Reminder>));
        private static readonly DispatcherTimer _timer;

        private static ObservableCollection<Reminder> Reminders { get; }
        public static ReadOnlyObservableCollection<Reminder> ReminderRO { get; }

        private static TimeSpan _timeElapsed;
        public static TimeSpan TimeElapsed {
            get => _timeElapsed;
            set { 
                _timeElapsed = value;

                TimeElapsedChanged?.Invoke();

                foreach (var reminder in Reminders) {
                    if (!reminder.Enabled) continue;

                    if (reminder.Time == TimeElapsed) Remind(reminder.Time);
                    else if (reminder.Behavior == ReminderBehavior.Repeat) {
                        double elapsedSeconds = TimeElapsed.TotalSeconds;
                        double reminderSeconds = reminder.Time.TotalSeconds;

                        if (elapsedSeconds > 0 && elapsedSeconds % reminderSeconds == 0)
                            Remind(reminder.Time, (int)(elapsedSeconds / reminderSeconds));
                    }
                }
            }
        }
        public static event Action? TimeElapsedChanged;

        public static bool IsEnabled => _timer.IsEnabled;
        public static event Action? IsEnabledChanged;

        static AppStopwatch() {
            Reminders = DeserializeReminders();
            Reminders.CollectionChanged += HandleRemindersChanged;
            ReminderRO = new(Reminders);

            _timer = new() {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Start();

            _timer.Tick += (object? sender, EventArgs e) => {
                TimeElapsed += TimeSpan.FromSeconds(1);
            };

            App.Current.Exit += HandleApplicationExit;
        }

        private static void HandleApplicationExit(object sender, ExitEventArgs e) => SerializeReminders();

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



        private static ObservableCollection<Reminder> DeserializeReminders() {
            if (!File.Exists(PathToRemindersXML)) return [];

            using StreamReader reader = new(PathToRemindersXML);
            ObservableCollection<Reminder>? deser = _remindersSerializer.Deserialize(reader) as ObservableCollection<Reminder>;
            if (deser != null) foreach (var reminder in deser) reminder.PropertyChanged += SingleReminderChanged; 

            return deser ?? [];
        }

        private static void Remind(TimeSpan time, int times = 1) {
            string text = times > 1 ? $"Hey, another {time} has passed ({times})" : $"Hey, {time} has passed";

            ReminderWindow remWin = new();
            remWin.DataContext = new ReminderVM(text, remWin);
            remWin.Show();
            SystemSounds.Beep.Play();
        }

        private static void SerializeReminders() {
            using StreamWriter writer = new(PathToRemindersXML);
            _remindersSerializer.Serialize(writer, Reminders);
        }


        #region Public API

        public static void Stop() {
            _timer.Stop();
            IsEnabledChanged?.Invoke();
        }


        public static void Resume() { 
            _timer.Start();
            IsEnabledChanged?.Invoke();
        }

        public static void Restart() {
            _timer.Stop();
            _timer.Start();
            TimeElapsed = TimeSpan.Zero;
            IsEnabledChanged?.Invoke();
        }

        public static void DeleteReminder(Reminder target) { 
            Reminders.Remove(target);
            SerializeReminders();
        }

        public static void AddReminder(TimeSpan time, ReminderBehavior behavior, bool enabled) {
            Reminders.Add(new(time, behavior, enabled));
        }

        #endregion
    }
}
