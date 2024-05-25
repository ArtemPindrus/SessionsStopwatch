using SessionsStopwatch.Commands;
using System.Windows.Input;

namespace SessionsStopwatch.ViewModels
{
    class StartStopwatchVM(NavigationStore navigationStore) : ViewModelBase {
        public ICommand StartStopwatchCommand { get; } = new StartStopwatchCommand(navigationStore);
    }
}
