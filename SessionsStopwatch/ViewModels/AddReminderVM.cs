using SessionsStopwatch.Commands;
using SessionsStopwatch.Utilities;
using SessionsStopwatch.Windows;
using System.Windows.Input;

namespace SessionsStopwatch.ViewModels {
    /// <summary>
    /// ViewModel of <see cref="AddReminderWindow"/>.
    /// </summary>
    internal class AddReminderVM : ViewModelBase {
        private string? hoursBox = "0";
        private string? minutesBox = "0";
        private string? secondsBox = "0";
        private ReminderBehavior? selectedBehavior;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddReminderVM"/> class.
        /// </summary>
        /// <param name="associatedWindow">Associated <see cref="AddReminderWindow"/>.</param>
        public AddReminderVM(AddReminderWindow associatedWindow) {
            CloseWindowCommand = new CloseWindowCommand(associatedWindow);
            AddReminderCommand = new AddReminderCommand(this, associatedWindow);
        }

        /// <summary>
        /// Gets command used to close associated <see cref="AddReminderWindow"/>.
        /// </summary>
        public ICommand CloseWindowCommand { get; }

        /// <summary>
        /// Gets command to confirm <see cref="Reminder"/> addition.
        /// </summary>
        public ICommand AddReminderCommand { get; }

        /// <summary>
        /// Gets or sets a value indicating whether new <see cref="Reminder"/> should be initially enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets a full time value of <see cref="hoursBox"/>, <see cref="minutesBox"/> and <see cref="secondsBox"/> combined.
        /// </summary>
        public string FullTimeBox => $"{HoursBox}:{MinutesBox}:{SecondsBox}";

        /// <summary>
        /// Gets or sets value of the hours box.
        /// </summary>
        public string? HoursBox {
            get => hoursBox;
            set {
                hoursBox = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(FullTimeBox));
            }
        }

        /// <summary>
        /// Gets or sets value of the minutes box.
        /// </summary>
        public string? MinutesBox {
            get => minutesBox;
            set {
                minutesBox = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(FullTimeBox));
            }
        }

        /// <summary>
        /// Gets or sets value of the seconds box.
        /// </summary>
        public string? SecondsBox {
            get => secondsBox;
            set {
                secondsBox = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(FullTimeBox));
            }
        }

        /// <summary>
        /// Gets or sets what the new <see cref="Reminder"/> behavior should be.
        /// </summary>
        public ReminderBehavior? SelectedBehavior {
            get => selectedBehavior;
            set {
                selectedBehavior = value;
                NotifyPropertyChanged();
            }
        }
    }
}