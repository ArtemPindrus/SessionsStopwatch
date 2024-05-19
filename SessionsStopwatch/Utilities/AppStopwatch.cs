using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace SessionsStopwatch.Utilities
{
    static class AppStopwatch {
        private const string PathToRemindersXML = @"reminders.xml";
        private static readonly DispatcherTimer _timer;
        private static readonly XmlSerializer _remindersSerializer = new(typeof(ObservableCollection<Reminder>));

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
        }

#warning Doesn't serialize on item change
        public static void SerializeReminders() {
            using StreamWriter writer = new(PathToRemindersXML);
            _remindersSerializer.Serialize(writer, Reminders);
        }

        private static ObservableCollection<Reminder> DeserializeReminders() {
            if (!File.Exists(PathToRemindersXML)) return [];

            using StreamReader reader = new(PathToRemindersXML);
            ObservableCollection<Reminder>? deser = _remindersSerializer.Deserialize(reader) as ObservableCollection<Reminder>;

            return deser ?? [];
        }

        private static void OnTimeElapsedChanged() {
            TimeElapsedChanged?.Invoke();

            foreach (var reminder in Reminders) {
                if (reminder.Time == TimeElapsed) Remind(reminder.Time);
                else if (reminder.Behavior == ReminderBehavior.Repeat 
                    && TimeElapsed.TotalSeconds > 0 && TimeElapsed.TotalSeconds % reminder.Time.TotalSeconds == 0) Remind(reminder.Time, true);
            }
        }

        private static void Remind(TimeSpan time, bool again = false) {
#warning notImpl
            MessageBox.Show(again ? $"Hey another {time} has passed!" : $"Hey {time} has passed!");
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
