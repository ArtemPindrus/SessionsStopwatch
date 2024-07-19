using SessionsStopwatch.ViewModels;
using StoreApp.Commands;

namespace SessionsStopwatch.Commands {
    /// <summary>
    /// Switches the tray state of <see cref="MainWindow"/>.
    /// </summary>
    /// <param name="viewModel">Associated <see cref="MainViewModel"/>.</param>
    /// <param name="toTray"><see cref="bool"/> indicating whether command will set window to tray (<see cref="true"/>), or out of tray (if <see cref="false"/>)</param>
    internal class TraySwitchCommand(MainViewModel viewModel, bool toTray) : CommandBase {
        private readonly MainViewModel vm = viewModel;
        private readonly bool toTray = toTray;

        /// <summary>
        /// Switches the tray state of <see cref="MainWindow"/>.
        /// </summary>
        /// <param name="parameter"><inheritdoc/></param>
        public override void Execute(object? parameter) {
            vm.WindowVisibility = toTray ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible;
        }
    }
}