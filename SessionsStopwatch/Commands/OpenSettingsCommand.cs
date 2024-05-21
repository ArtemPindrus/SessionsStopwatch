using SessionsStopwatch.ViewModels;
using StoreApp.Commands;
using System.Windows;

namespace SessionsStopwatch.Commands {
    public class OpenSettingsCommand : CommandBase {
        private readonly SettingsWindow _settingsWindow;

        public OpenSettingsCommand(SettingsWindow settingsWindow) => _settingsWindow = settingsWindow;

        public override void Execute(object? parameter) {
            _settingsWindow.Show();
        }
    }
}
