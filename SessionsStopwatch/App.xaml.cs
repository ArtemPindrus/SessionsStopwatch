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
            AppStopwatch.Stop();

            _navigationStore.CurrentViewModel = new StartStopwatchVM(_navigationStore);

            MainWindow = new MainWindow() {
                DataContext = new MainViewModel(_navigationStore),
            };

            MainWindow.Show();

            SystemEvents.PowerModeChanged += OnPowerModeChanged;

            base.OnStartup(e);
        }

        private void OnPowerModeChanged(object sender, PowerModeChangedEventArgs e) {
            if (e.Mode == PowerModes.Suspend) {
                AppStopwatch.Stop();
                _navigationStore.CurrentViewModel = new StartStopwatchVM(_navigationStore);

                MainWindow.Visibility = Visibility.Visible;
            } else if (e.Mode == PowerModes.Resume) {
                AppStopwatch.Restart();
            }
        }
    }
}
