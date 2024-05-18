using StoreApp.Commands;
using System.Windows;

namespace SessionsStopwatch.ViewModels
{
    class CloseWindowCommand : CommandBase {
        private readonly Window _window;

        public CloseWindowCommand(Window window) { 
            _window = window;
        }

        public override void Execute(object? parameter) {
            if (_window == Application.Current.MainWindow) {
                Application.Current.Shutdown();
            } else _window.Close();
        }
    }
}
