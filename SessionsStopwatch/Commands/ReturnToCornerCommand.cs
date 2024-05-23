using StoreApp.Commands;
using System.Windows;
using SessionsStopwatch.ViewModels;

namespace SessionsStopwatch.Commands
{
    class ReturnToCornerCommand(MainViewModel vm) : CommandBase {
        private readonly MainViewModel _vm = vm;

        public override void Execute(object? parameter) {
            Application.Current.MainWindow.Left = SystemParameters.WorkArea.Width - Application.Current.MainWindow.Width;
            Application.Current.MainWindow.Top = SystemParameters.WorkArea.Height - Application.Current.MainWindow.Height;
            _vm.WindowVisibility = Visibility.Visible;
        }
    }
}
