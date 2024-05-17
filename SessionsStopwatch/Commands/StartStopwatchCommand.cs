using SessionsStopwatch.Utilities;
using SessionsStopwatch.ViewModels;
using StoreApp.Commands;

namespace SessionsStopwatch.Commands
{
    class StartStopwatchCommand : CommandBase {
        private readonly NavigationStore _navigationStore;

        public StartStopwatchCommand(NavigationStore navigationStore) { 
            _navigationStore = navigationStore;
        }

        public override void Execute(object? parameter) {
            _navigationStore.CurrentViewModel = new StopwatchViewModel();
            AppStopwatch.Restart();
        }
    }
}
