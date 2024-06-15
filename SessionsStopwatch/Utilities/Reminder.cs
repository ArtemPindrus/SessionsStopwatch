using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SessionsStopwatch.Utilities
{
    public enum ReminderBehavior { OneTime, Repeat }

    public partial class Reminder : INotifyPropertyChanged {
        public Reminder(TimeSpan time, ReminderBehavior behavior, bool enabled = false) {
            Time = time;
            Behavior = behavior;
            Enabled = enabled;
        }

        public Reminder() { 
            Time = TimeSpan.FromSeconds(1);
            Behavior = ReminderBehavior.OneTime;
        }

        private bool _enabled;
        public bool Enabled {
            get => _enabled;
            set { 
                _enabled = value;
                NotifyPropertyChanged();
            }
        }


        private TimeSpan _time;
        public TimeSpan Time {
            get => _time;
            set { 
                _time = value;
                NotifyPropertyChanged();
            }
        }

        private ReminderBehavior _behavior;
        public ReminderBehavior Behavior {
            get => _behavior;
            set {
                if (_behavior != value) { 
                    _behavior = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
