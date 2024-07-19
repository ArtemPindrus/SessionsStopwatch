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

namespace SessionsStopwatch.Utilities {
    /// <summary>
    /// Utility used to track time in application.
    /// </summary>
    internal static class AppStopwatch {
        private const string PathToRemindersXML = @"reminders.xml";
        private static readonly XmlSerializer RemindersSerializer = new(typeof(ObservableCollection<Reminder>));
        private static readonly DispatcherTimer Timer;

        private static TimeSpan timeElapsed;

        static AppStopwatch() {
            Reminders = DeserializeReminders();
            Reminders.CollectionChanged += HandleRemindersCollectionChanged;
            ReminderRO = new(Reminders);

            Timer = new() {
                Interval = TimeSpan.FromSeconds(1),
            };
            Timer.Start();

            Timer.Tick += (object? sender, EventArgs e) => {
                TimeElapsed += TimeSpan.FromSeconds(1);
            };

            App.Current.Exit += HandleApplicationExit;
        }

        /// <summary>
        /// Invoked when <see cref="TimeElapsed"/> value is changed.
        /// </summary>
        public static event Action? TimeElapsedChanged;

        /// <summary>
        /// Invoked when <see cref="IsEnabled"/> is changed.
        /// </summary>
        public static event Action? IsEnabledChanged;

        /// <summary>
        /// Gets accumulated time of the stopwatch.
        /// </summary>
        public static TimeSpan TimeElapsed {
            get => timeElapsed;
            private set {
                timeElapsed = value;

                TimeElapsedChanged?.Invoke();

                foreach (var reminder in Reminders) {
                    if (!reminder.Enabled) continue;

                    if (reminder.Time == TimeElapsed) {
                        Remind(reminder);
                    } else if (reminder.Behavior == ReminderBehavior.Repeat) {
                        double elapsedSeconds = TimeElapsed.TotalSeconds;
                        double reminderSeconds = reminder.Time.TotalSeconds;

                        if (elapsedSeconds > 0 && elapsedSeconds % reminderSeconds == 0)
                            Remind(reminder);
                    }
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether <see cref="AppStopwatch"/> is enabled.
        /// </summary>
        public static bool IsEnabled => Timer.IsEnabled;

        /// <summary>
        /// Gets <see cref="Reminders"/> as a ReadOnlyCollection.
        /// </summary>
        public static ReadOnlyObservableCollection<Reminder> ReminderRO { get; }

        private static ObservableCollection<Reminder> Reminders { get; }

        /// <summary>
        /// Stops time accumulation.
        /// </summary>
        public static void Stop() {
            Timer.Stop();
            IsEnabledChanged?.Invoke();
        }

        /// <summary>
        /// Resumes time accumulation.
        /// </summary>
        public static void Resume() {
            Timer.Start();
            IsEnabledChanged?.Invoke();
        }

        /// <summary>
        /// Sets <see cref="TimeElapsed"/> to zero and starts time accumulation.
        /// </summary>
        public static void Restart() {
            Timer.Stop();
            Timer.Start();
            TimeElapsed = TimeSpan.Zero;
            IsEnabledChanged?.Invoke();
        }

        /// <summary>
        /// Adds a <see cref="Reminder"/> to app.
        /// </summary>
        /// <param name="time">Time of reminder.</param>
        /// <param name="behavior">Behaviour of reminder.</param>
        /// <param name="enabled">Is reminder initially enabled?.</param>
        public static void AddReminder(TimeSpan time, ReminderBehavior behavior, bool enabled) {
            Reminders.Add(new(time, behavior, enabled));
        }

        /// <summary>
        /// Deletes a <see cref="Reminder"/> from app.
        /// </summary>
        /// <param name="target"><see cref="Reminder"/> to delete.</param>
        public static void DeleteReminder(Reminder target) {
            Reminders.Remove(target);
        }

        private static void HandleApplicationExit(object sender, ExitEventArgs e) => SerializeReminders();

        private static void HandleRemindersCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
            SerializeReminders();

            if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null) {
                foreach (var newReminder in e.NewItems) {
                    ((Reminder)newReminder).PropertyChanged += SingleReminderChanged;
                }
            } else if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems != null) {
                foreach (var oldReminder in e.OldItems) {
                    ((Reminder)oldReminder).PropertyChanged -= SingleReminderChanged;
                }
            }
        }

        private static void SingleReminderChanged(object? sender, PropertyChangedEventArgs e) {
            SerializeReminders();
        }

        /// <summary>
        /// Logic that gets invoked when <see cref="Reminder"/> triggers.
        /// </summary>
        /// <param name="triggered">Triggered <see cref="Reminder"/>.</param>
        private static void Remind(Reminder triggered) {
            double elapsedSeconds = TimeElapsed.TotalSeconds;
            double reminderSeconds = triggered.Time.TotalSeconds;
            int times = (int)(elapsedSeconds / reminderSeconds);

            string text = times > 1 ? $"Hey, another {triggered.Time} has passed ({times})" : $"Hey, {triggered.Time} has passed";

            ReminderWindow remWin = new();
            remWin.DataContext = new ReminderVM(text, remWin);
            remWin.Show();
            SystemSounds.Beep.Play();
        }

        private static void SerializeReminders() {
            using StreamWriter writer = new(PathToRemindersXML);
            RemindersSerializer.Serialize(writer, Reminders);
        }

        private static ObservableCollection<Reminder> DeserializeReminders() {
            if (!File.Exists(PathToRemindersXML)) return [];

            using StreamReader reader = new(PathToRemindersXML);
            ObservableCollection<Reminder>? deser = RemindersSerializer.Deserialize(reader) as ObservableCollection<Reminder>;
            if (deser != null) foreach (var reminder in deser) reminder.PropertyChanged += SingleReminderChanged;

            return deser ?? [];
        }
    }
}