using SessionsStopwatch.Utilities;
using StoreApp.Commands;

namespace SessionsStopwatch.Commands {
    /// <summary>
    /// Switches <see cref="AppStopwatch"/> state.
    /// </summary>
    internal class SwitchStopwatchState : CommandBase {
        /// <summary>
        /// Switches <see cref="AppStopwatch"/> state.
        /// </summary>
        /// <param name="parameter"><inheritdoc/></param>
        public override void Execute(object? parameter) {
            if (AppStopwatch.IsEnabled) AppStopwatch.Stop();
            else AppStopwatch.Resume();
        }
    }
}