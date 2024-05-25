using SessionsStopwatch.Windows;
using System.Windows.Input;

namespace SessionsStopwatch.ViewModels {
    public class ReminderVM(string displayText, ReminderWindow associatedWindow) : ViewModelBase {
        public string? DisplayText { get; } = displayText;
        public ICommand CloseCommand { get; } = new CloseWindowCommand(associatedWindow);
    }
}
