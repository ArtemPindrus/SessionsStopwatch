using StoreApp.Commands;
using System.Windows;

namespace SessionsStopwatch.Commands {
    internal class HideWindowCommand : CommandBase {
        private readonly Window _window;

        public HideWindowCommand(Window window) {
            _window = window;
        }

        public override void Execute(object? parameter) { 
            _window.Hide();
        }
    }
}
