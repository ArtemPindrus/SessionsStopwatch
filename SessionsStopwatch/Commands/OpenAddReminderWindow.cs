using SessionsStopwatch.ViewModels;
using SessionsStopwatch.Windows;
using StoreApp.Commands;

namespace SessionsStopwatch.Commands {
    /// <summary>
    /// Opens <see cref="AddReminderWindow"/>, creating a new instance of it.
    /// </summary>
    internal class OpenAddReminderWindow : CommandBase {
        /// <summary>
        /// Opens new <see cref="AddReminderWindow"/>.
        /// </summary>
        /// <param name="parameter"><inheritdoc/></param>
        public override void Execute(object? parameter) {
            AddReminderWindow window = new();
            AddReminderVM vm = new(window);
            window.DataContext = vm;
            window.Show();
        }
    }
}