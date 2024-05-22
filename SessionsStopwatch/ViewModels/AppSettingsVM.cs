using SessionsStopwatch.Commands;
using SessionsStopwatch.Utilities;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

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

                    if (value == true) WindowUtility.LimitToScreenBounds(App.Current.MainWindow);
                }
            }
        }

        public bool Startup { 
            get => RegistryRunKeyHelper.IsInRunKey;
            set {
                if (RegistryRunKeyHelper.IsInRunKey != value) {
                    if (value == true) RegistryRunKeyHelper.AddAppToRunKey();
                    else RegistryRunKeyHelper.RemoveAppToRunKey();

                    NotifyPropertyChanged();
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

        public ICommand HideWindowCommand { get; }

        public AppSettingsVM(SettingsWindow associatedWindow) {
            HideWindowCommand = new ChangeWindowVisibility(associatedWindow);
        }
    }
}
