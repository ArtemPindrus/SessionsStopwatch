using SessionsStopwatch.Commands;
using SessionsStopwatch.Utilities;
using System.Windows;
using System.Windows.Input;

namespace SessionsStopwatch.ViewModels
{
    class AddAReminderVM : ViewModelBase
    {
        public AddAReminderVM(Window associatedWindow) {
            CloseWindowCommand = new CloseWindowCommand(associatedWindow);
            AddReminderCommand = new AddReminderCommand(this, associatedWindow);
        }

        public ICommand CloseWindowCommand { get; }
        public ICommand AddReminderCommand { get; }

        public bool Enabled { get; set; }

        public string FullTimeBox => $"{HoursBox}:{MinutesBox}:{SecondsBox}";

        private string? _hoursBox = "0";
        public string? HoursBox {
            get => _hoursBox;
            set {
                _hoursBox = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(FullTimeBox));
            }
        }

        private string? _minutesBox = "0";
        public string? MinutesBox {
            get => _minutesBox;
            set {
                _minutesBox = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(FullTimeBox));
            }
        }

        private string? _secondsBox = "0";
        public string? SecondsBox {
            get => _secondsBox;
            set {
                _secondsBox = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(FullTimeBox));
            }
        }

        public ReminderBehavior? _selectedBehavior;
        public ReminderBehavior? SelectedBehavior {
            get => _selectedBehavior;
            set { 
                _selectedBehavior = value;
                NotifyPropertyChanged();
            }
        }
    }
}
