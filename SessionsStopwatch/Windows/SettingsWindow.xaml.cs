using SessionsStopwatch.Utilities;
using System.Windows;
using System.Windows.Input;

namespace SessionsStopwatch {
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window {
        public SettingsWindow() {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void Window_SourceInitialized(object sender, EventArgs e) {
            WindowAspectRatio.Register(this);
        }
    }
}
