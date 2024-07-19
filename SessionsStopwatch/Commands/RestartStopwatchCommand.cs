using SessionsStopwatch.Utilities;
using StoreApp.Commands;

namespace SessionsStopwatch.Commands {
    /// <summary>
    /// Command calls <see cref="AppStopwatch.Restart"/>.
    /// </summary>
    internal class RestartStopwatchCommand : CommandBase {
        /// <summary>
        /// Restarts <see cref="AppStopwatch"/>.
        /// </summary>
        /// <param name="parameter"><inheritdoc/></param>
        public override void Execute(object? parameter) {
            AppStopwatch.Restart();
        }
    }
}