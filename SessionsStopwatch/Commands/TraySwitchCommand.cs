using SessionsStopwatch.ViewModels;
using StoreApp.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionsStopwatch.Commands
{
    class TraySwitchCommand : CommandBase{
        private readonly MainViewModel _vm;
        private readonly bool _toTray;

        public TraySwitchCommand(MainViewModel viewModel, bool toTray) {
            _vm = viewModel;
            _toTray = toTray;
        }

        public override void Execute(object? parameter) {
            _vm.WindowVisibility = _toTray ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible;
        }
    }
}
