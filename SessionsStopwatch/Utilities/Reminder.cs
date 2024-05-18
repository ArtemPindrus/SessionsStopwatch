using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace SessionsStopwatch.Utilities
{
    public enum ReminderBehavior { OneTime, Repeat }

    public partial class Reminder : INotifyPropertyChanged {
        public Reminder(TimeSpan time, ReminderBehavior behavior) {
            Time = time;
            Behavior = behavior;
        }

        public Reminder() { 
            Time = TimeSpan.FromSeconds(1);
            Behavior = ReminderBehavior.OneTime;
        }

        private TimeSpan _time;
        public TimeSpan Time {
            get => _time;
            set { 
                _time = value;
                NotifyPropertyChanged();
            }
        }

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

        public ReminderBehavior Behavior { 
            get; 
            set; 
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        [GeneratedRegex(@"^([01][0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$")]
        private static partial Regex TimeRegex();
    }
}
