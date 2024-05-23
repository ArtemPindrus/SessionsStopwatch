using StoreApp.Commands;
using System.Windows;

namespace SessionsStopwatch.ViewModels
{
    class CloseWindowCommand(Window window) : CommandBase {
        private readonly Window _window = window;

        public override void Execute(object? parameter) {
            if (_window == Application.Current.MainWindow) {
                Application.Current.Shutdown();
            } else _window.Close();
        }
    }
}
