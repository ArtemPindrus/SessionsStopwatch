using SessionsStopwatch.Utilities;
using SessionsStopwatch.ViewModels;
using StoreApp.Commands;

namespace SessionsStopwatch.Commands
{
    class StartStopwatchCommand(NavigationStore navigationStore) : CommandBase {
        private readonly NavigationStore _navigationStore = navigationStore;

        public override void Execute(object? parameter) {
            _navigationStore.CurrentViewModel = new StopwatchViewModel();
            AppStopwatch.Restart();
        }
    }
}
