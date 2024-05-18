using SessionsStopwatch.ViewModels;
using StoreApp.Commands;

namespace SessionsStopwatch.Commands {
    public class OpenSettingsCommand : CommandBase {
        public override void Execute(object? parameter) {
            SettingsWindow settingsWindow = new();
            settingsWindow.Show();

            settingsWindow.DataContext = new AppSettingsVM(settingsWindow);
        }
    }
}
