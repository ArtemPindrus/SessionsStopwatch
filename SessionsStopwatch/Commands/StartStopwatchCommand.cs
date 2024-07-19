using SessionsStopwatch.Utilities;
using SessionsStopwatch.ViewModels;
using StoreApp.Commands;

namespace SessionsStopwatch.Commands {
    /// <summary>
    /// Command starts <see cref="AppStopwatch"/>
    /// and sets <see cref="NavigationStore.currentViewModel"/> to new instance of <see cref="StopwatchViewModel"/>.
    /// </summary>
    /// <param name="navigationStore">Instance of <see cref="NavigationStore"/> to change current view to <see cref="StopwatchViewModel"/>.</param>
    internal class StartStopwatchCommand(NavigationStore navigationStore) : CommandBase {
        private readonly NavigationStore navigationStore = navigationStore;

        /// <summary>
        /// Starts <see cref="AppStopwatch"/>
        /// and sets <see cref="NavigationStore.currentViewModel"/> to new instance of <see cref="StopwatchViewModel"/>.
        /// </summary>
        /// <param name="parameter"><inheritdoc/></param>
        public override void Execute(object? parameter) {
            navigationStore.CurrentViewModel = new StopwatchViewModel();
            AppStopwatch.Restart();
        }
    }
}