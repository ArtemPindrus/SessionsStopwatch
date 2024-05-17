using SessionsStopwatch.ViewModels;
using StoreApp.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionsStopwatch.Commands
{
    class ToTrayCommand : CommandBase{
        private readonly MainViewModel _vm;

        public ToTrayCommand(MainViewModel viewModel) {
            _vm = viewModel;
        }

        public override void Execute(object? parameter) {
            _vm.WindowVisibility = System.Windows.Visibility.Hidden;
        }
    }
}
