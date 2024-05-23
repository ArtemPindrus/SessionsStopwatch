using SessionsStopwatch.ViewModels;
using StoreApp.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionsStopwatch.Commands
{
    class TraySwitchCommand(MainViewModel viewModel, bool toTray) : CommandBase {
        private readonly MainViewModel _vm = viewModel;
        private readonly bool _toTray = toTray;

        public override void Execute(object? parameter) {
            _vm.WindowVisibility = _toTray ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible;
        }
    }
}
