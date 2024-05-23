using StoreApp.Commands;
using System.Windows;

namespace SessionsStopwatch.Commands {
    internal class ChangeWindowVisibility(Window window) : CommandBase {
        private readonly Window _window = window;

        public override void Execute(object? parameter) {
            if(_window.IsVisible) _window.Hide();
            else _window.Show();
        }
    }
}
