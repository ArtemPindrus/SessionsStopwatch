using SessionsStopwatch.Utilities;
using StoreApp.Commands;

namespace SessionsStopwatch.Commands
{
    class RestartStopwatchCommand : CommandBase {
        public override void Execute(object? parameter) {
            AppStopwatch.Restart();
        }
    }
}
