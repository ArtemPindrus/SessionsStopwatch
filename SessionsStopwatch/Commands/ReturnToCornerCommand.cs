using SessionsStopwatch.Utilities;
using SessionsStopwatch.ViewModels;
using StoreApp.Commands;
using System.Windows;

namespace SessionsStopwatch.Commands {
    /// <summary>
    /// Command puts <see cref="MainWindow"/> to the right-bottom corner of the screen.
    /// </summary>
    /// <param name="vm">Associated <see cref="MainViewModel"/>.</param>
    internal class ReturnToCornerCommand(MainViewModel vm) : CommandBase {
        private readonly MainViewModel vm = vm;

        /// <summary>
        /// Puts <see cref="MainWindow"/> to the right-bottom corner of the screen.
        /// </summary>
        /// <param name="parameter"><inheritdoc/></param>
        public override void Execute(object? parameter) {
            WindowUtility.ToTheRightBottomCorner(Application.Current.MainWindow);
            vm.WindowVisibility = Visibility.Visible;
        }
    }
}