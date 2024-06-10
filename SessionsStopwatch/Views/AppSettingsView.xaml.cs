using SessionsStopwatch.Utilities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SessionsStopwatch.Views {
    /// <summary>
    /// Interaction logic for AppSettingsView.xaml
    /// </summary>
    public partial class AppSettingsView : UserControl {
        public AppSettingsView() {
            InitializeComponent();
        }

        private void RemindersDataGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            DependencyObject? originalSource = (DependencyObject)e.OriginalSource;
            if (e.ChangedButton == MouseButton.Right) return; 

            while (originalSource != null && originalSource is not DataGridCell) {
                originalSource = VisualTreeHelper.GetParent(originalSource);
            }

            if (originalSource is DataGridCell cell) {
                if (cell.Content is CheckBox) {
                    DataGridRow row = DataGridRow.GetRowContainingElement(cell);

                    if (row.Item is not Reminder clicked) return;

                    clicked.Enabled = !clicked.Enabled;
                }

                e.Handled = true;
            }
        }
    }
}
