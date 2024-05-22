using SessionsStopwatch.Utilities;
using System.Windows;

namespace SessionsStopwatch {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            WindowUtility.ToTheRightBottomCorner(this);
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            DragMove();
        }

        private void Window_LocationChanged(object sender, EventArgs e) {
            if (AppSettings.Default.LimitToMonitor) WindowUtility.LimitToScreenBounds(this);
        }
    }
}