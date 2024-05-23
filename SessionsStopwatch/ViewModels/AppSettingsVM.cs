using SessionsStopwatch.Commands;
using SessionsStopwatch.Utilities;
using System.Windows.Input;

namespace SessionsStopwatch.ViewModels {
    public class AppSettingsVM(SettingsWindow associatedWindow) : ViewModelBase {
        public ICommand HideWindowCommand { get; } = new ChangeWindowVisibility(associatedWindow);


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
    }
}
