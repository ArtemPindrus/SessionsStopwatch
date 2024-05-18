using SessionsStopwatch.ViewModels;
using StoreApp.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionsStopwatch.Commands {
    public class OpenSettingsCommand : CommandBase {
        public override void Execute(object? parameter) {
            SettingsWindow settingsWindow = new();
            settingsWindow.Show();

            settingsWindow.DataContext = new AppSettingsVM(settingsWindow);
        }
    }
}
