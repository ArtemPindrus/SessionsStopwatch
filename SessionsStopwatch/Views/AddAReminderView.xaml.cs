using SessionsStopwatch.Utilities;
using System.Windows.Controls;

namespace SessionsStopwatch.Views
{
    /// <summary>
    /// Interaction logic for AddAReminderView.xaml
    /// </summary>
    public partial class AddAReminderView : UserControl
    {
        public AddAReminderView()
        {
            InitializeComponent();
        }

        private void HoursBox_Initialized(object sender, EventArgs e) {
            OnlyIntTextBox.Register((TextBox)sender, 0, 23);
        }

        private void MinutesBox_Initialized(object sender, EventArgs e) {
            OnlyIntTextBox.Register((TextBox)sender, 0, 59);
        }

        private void SecondsBox_Initialized(object sender, EventArgs e) {
            OnlyIntTextBox.Register((TextBox)sender, 0, 59);
        }
    }
}
