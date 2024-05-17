using SessionsStopwatch.Utilities;
using System.Windows;

namespace SessionsStopwatch {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            ToTheCorner();
        }

        private void ToTheCorner() {
            Left = SystemParameters.WorkArea.Width - Width;
            Top = SystemParameters.WorkArea.Height - SizingConst.WindowHeightNoHeader;
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            DragMove();
        }

        private void Window_LocationChanged(object sender, EventArgs e) {
            if (!AppSettings.Default.LimitToMonitor) return;

            if (Left < 0) Left = 0;
            else if (Left > SystemParameters.WorkArea.Width - Width) Left = SystemParameters.WorkArea.Width - Width;

            if (Top < 0) Top = 0;
            else if (Top > SystemParameters.WorkArea.Height - Height)
                Top = SystemParameters.WorkArea.Height - Height;
        }
    }
}