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
    }
}
