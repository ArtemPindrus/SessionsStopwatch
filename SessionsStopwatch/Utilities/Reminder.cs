using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SessionsStopwatch.Utilities {
    /// <summary>
    /// Represents how <see cref="Reminder"/> should trigger.
    /// </summary>
    public enum ReminderBehavior {
        /// <summary>
        /// Represents behavior when <see cref="Reminder"/> should trigger only once
        /// when <see cref="AppStopwatch.TimeElapsed"/> reaches exactly <see cref="Reminder.Time"/>.
        /// </summary>
        OneTime,

        /// <summary>
        /// Represents behavior when <see cref="Reminder"/> should trigger every time an <see cref="Reminder.Time"/> time elapses.
        /// </summary>
        Repeat,
    }

    /// <summary>
    /// Instances of class are used to remind user about computer usage.
    /// </summary>
    public class Reminder : INotifyPropertyChanged {
        private bool enabled;
        private TimeSpan time;
        private ReminderBehavior behavior;

        /// <summary>
        /// Initializes a new instance of the <see cref="Reminder"/> class.
        /// </summary>
        /// <param name="time">When reminder should be triggered.</param>
        /// <param name="behavior">How reminder should act on trigger.</param>
        /// <param name="enabled">Should the reminder be initially enabled?.</param>
        public Reminder(TimeSpan time, ReminderBehavior behavior, bool enabled = false) {
            Time = time;
            Behavior = behavior;
            Enabled = enabled;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Reminder"/> class with default values.
        /// </summary>
        public Reminder() {
            Time = TimeSpan.FromSeconds(1);
            Behavior = ReminderBehavior.OneTime;
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="Reminder"/> should be triggered.
        /// </summary>
        public bool Enabled {
            get => enabled;
            set {
                enabled = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets when the <see cref="Reminder"/> should be triggered.
        /// </summary>
        public TimeSpan Time {
            get => time;
            set {
                time = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets how <see cref="Reminder"/> should be triggered.
        /// </summary>
        public ReminderBehavior Behavior {
            get => behavior;
            set {
                if (behavior != value) {
                    behavior = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}