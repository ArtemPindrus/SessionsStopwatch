using StoreApp.Commands;

namespace SessionsStopwatch.ViewModels
{
    class CloseCommand : CommandBase {
        public override void Execute(object? parameter) {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
