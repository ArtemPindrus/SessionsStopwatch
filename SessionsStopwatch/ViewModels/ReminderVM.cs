using SessionsStopwatch.Windows;
using System.Windows.Input;

namespace SessionsStopwatch.ViewModels {
    public class ReminderVM : ViewModelBase {
		public string? DisplayText { get; }
		public ICommand CloseCommand { get; }


		public ReminderVM(string displayText, ReminderWindow associatedWindow) { 
			DisplayText = displayText;
			CloseCommand = new CloseWindowCommand(associatedWindow);
		}
	}
}
