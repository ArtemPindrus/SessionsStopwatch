using StoreApp.Commands;
using System.Windows;

namespace SessionsStopwatch.Commands {
    /// <summary>
    /// Command used to close a <see cref="Window"/>.
    /// </summary>
    /// <param name="window">Associated <see cref="Window"/>.</param>
    public class CloseWindowCommand(Window window) : CommandBase {
        private readonly Window window = window;

        /// <summary>
        /// Closes <see cref="window"/>.
        /// </summary>
        /// <param name="parameter"><inheritdoc/></param>
        public override void Execute(object? parameter) {
            if (window == Application.Current.MainWindow) {
                Application.Current.Shutdown();
            } else {
                window.Close();
            }
        }
    }
}