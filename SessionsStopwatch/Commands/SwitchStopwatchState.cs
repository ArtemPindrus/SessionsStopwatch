using SessionsStopwatch.Utilities;
using StoreApp.Commands;

namespace SessionsStopwatch.Commands
{
    class SwitchStopwatchState : CommandBase {
        public override void Execute(object? parameter) {
            if (AppStopwatch.IsEnabled) AppStopwatch.Stop();
            else AppStopwatch.Resume();
        }
    }
}
