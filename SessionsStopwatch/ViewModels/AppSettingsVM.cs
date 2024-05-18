using Microsoft.Win32;
using SessionsStopwatch.Commands;
using SessionsStopwatch.Utilities;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SessionsStopwatch.ViewModels {
    public class AppSettingsVM : ViewModelBase {
        public bool AutoStartOnSession {
            get => AppSettings.Default.AutoStartOnSession;
            set {
                if (AppSettings.Default.AutoStartOnSession != value) {
                    AppSettings.Default.AutoStartOnSession = value;
                    AppSettings.Default.Save();
                    NotifyPropertyChanged();
                }
            }
        }

        public bool LimitToMonitorBounds {
            get => AppSettings.Default.LimitToMonitor;
            set {
                if (AppSettings.Default.LimitToMonitor != value) {
                    AppSettings.Default.LimitToMonitor = value;
                    AppSettings.Default.Save();
                    NotifyPropertyChanged();
                }
            }
        }

        public bool Startup { 
            get => AppSettings.Default.Startup;
            set {
                if (AppSettings.Default.Startup != value) {
                    AppSettings.Default.Startup = value;
                    AppSettings.Default.Save();
                    NotifyPropertyChanged();

                    var keyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
                    string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    RegistryKey? key = Registry.CurrentUser.OpenSubKey(keyPath, true);

                    if (value == true) key?.SetValue("SessionStopwatch", appPath);
                    else key?.DeleteValue("SessionStopwatch", true);
                }
            }
        }

        private string? _addReminderTextBox;
        public string? AddReminderTextBox {
            get => _addReminderTextBox;
            set {
                _addReminderTextBox = value;
                AddReminderTextBoxChanged?.Invoke();
            }
        }
        public event Action? AddReminderTextBoxChanged;

        public ICommand CloseCommand { get; }

        public AppSettingsVM(SettingsWindow associatedWindow) {
            CloseCommand = new CloseWindowCommand(associatedWindow);
        }
    }
}
