using SessionsStopwatch.ViewModels;
using SessionsStopwatch.Windows;
using StoreApp.Commands;

namespace SessionsStopwatch.Commands
{
    class OpenAddReminderWindow : CommandBase {
        public override void Execute(object? parameter) {
            AddAReminderWindow window = new();
            AddAReminderVM vm = new(window);
            window.DataContext = vm;
            window.Show();
        }
    }
}
