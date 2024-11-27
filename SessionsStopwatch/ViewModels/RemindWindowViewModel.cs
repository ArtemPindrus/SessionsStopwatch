using CommunityToolkit.Mvvm.Input;
using SessionsStopwatch.Utilities;
using SessionsStopwatch.Views;

namespace SessionsStopwatch.ViewModels;

public partial class RemindWindowViewModel : ViewModelBase {
    public string ReminderText { get; }
    
    public RemindWindowViewModel(string reminderText) {
        ReminderText = reminderText;
    }
    
    [RelayCommand]
    private void Close() {
        WindowUtility.CloseFirst<RemindWindow>();
    }
}