using SessionsStopwatch.Commands;
using SessionsStopwatch.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SessionsStopwatch.ViewModels
{
    class StartStopwatchVM : ViewModelBase {
        public ICommand StartStopwatchCommand { get; }

        public StartStopwatchVM(NavigationStore navigationStore) {
            StartStopwatchCommand = new StartStopwatchCommand(navigationStore);
        }
    }
}
