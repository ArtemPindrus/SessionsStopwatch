using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

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

        [XmlIgnore]
        public string? StringyTime { 
            get => Time.ToString();
            set {
                Regex regex = TimeRegex();

                if (value != null && regex.IsMatch(value) && TimeSpan.TryParse(value, out TimeSpan res) && res.TotalSeconds > 0) { 
                    Time = res;
                }

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


        [GeneratedRegex(@"^([01][0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$")]
        private static partial Regex TimeRegex();

        //public static bool operator ==(Reminder lhs, Reminder rhs) { 
        //    return lhs.Time == rhs.Time && lhs.Behavior == rhs.Behavior;
        //}

        //public static bool operator !=(Reminder lhs, Reminder rhs) => !(lhs == rhs);

        //public override bool Equals(object? obj) {
        //    if (obj == null) return false;

        //    return ReferenceEquals(this, obj) || GetHashCode() == obj.GetHashCode();
        //}

        //public override int GetHashCode() {
        //    return (int)Time.TotalSeconds + (int)Behavior;
        //}
    }
}
