using Microsoft.Win32;
using SessionsStopwatch.Utilities;
using SessionsStopwatch.ViewModels;
using System.Windows;

namespace SessionsStopwatch {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private readonly NavigationStore _navigationStore = new();

        protected override void OnStartup(StartupEventArgs e) {
            if (AppSettings.Default.AutoStartOnSession) AppStopwatch.Restart();
            else AppStopwatch.Stop();

            _navigationStore.CurrentViewModel = AppSettings.Default.AutoStartOnSession ? 
                new StopwatchViewModel() : new StartStopwatchVM(_navigationStore);

            InstantiateMainWindow();

            SystemEvents.SessionSwitch += OnSessionSwitch;

            base.OnStartup(e);
        }

        private void InstantiateMainWindow() {
            MainWindow mainWindow = new();
            MainViewModel mainVM = new(_navigationStore);
            mainWindow.MouseEnter += mainVM.OnMouseEnter;
            mainWindow.MouseLeave += mainVM.OnMouseLeave;

            mainWindow.DataContext = mainVM;

            MainWindow = mainWindow;

            MainWindow.Show();
        }

        private void OnSessionSwitch(object sender, SessionSwitchEventArgs e) {
            if (e.Reason == SessionSwitchReason.SessionLock) {
                AppStopwatch.Stop();


                MainWindow.Visibility = Visibility.Visible;
            } else if (e.Reason == SessionSwitchReason.SessionUnlock) {
                bool autoStart = AppSettings.Default.AutoStartOnSession;
                if (autoStart) AppStopwatch.Restart();
                _navigationStore.CurrentViewModel = autoStart ? new StopwatchViewModel() : new StartStopwatchVM(_navigationStore);
            }
        }
    }
}
