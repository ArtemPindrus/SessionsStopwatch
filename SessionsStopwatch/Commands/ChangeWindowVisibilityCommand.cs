using StoreApp.Commands;
using System.Windows;

namespace SessionsStopwatch.Commands {
    /// <summary>
    /// Command used to switch <see cref="Window"/> visibility.
    /// </summary>
    /// <param name="window">Associated <see cref="Window"/>.</param>
    public class ChangeWindowVisibilityCommand(Window window) : CommandBase {
        private readonly Window window = window;

        /// <inheritdoc/>
        public override void Execute(object? parameter) {
            if (window.IsVisible) window.Hide();
            else window.Show();
        }
    }
}