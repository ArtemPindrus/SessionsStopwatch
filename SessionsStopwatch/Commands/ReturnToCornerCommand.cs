using StoreApp.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows;
using SessionsStopwatch.ViewModels;

namespace SessionsStopwatch.Commands
{
    class ReturnToCornerCommand : CommandBase {
        private readonly MainViewModel _vm;

        public ReturnToCornerCommand(MainViewModel vm) {
            _vm = vm;
        }

        public override void Execute(object? parameter) {
            Application.Current.MainWindow.Left = SystemParameters.WorkArea.Width - Application.Current.MainWindow.Width;
            Application.Current.MainWindow.Top = SystemParameters.WorkArea.Height - Application.Current.MainWindow.Height;
            _vm.WindowVisibility = Visibility.Visible;
        }
    }
}
