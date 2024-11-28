using System;

namespace SessionsStopwatch.ViewModels.Reminders;

public abstract class AddReminderBaseVM : ViewModelBase {
    public event Action? AddedReminder;

    protected void OnAddedReminder() {
        AddedReminder?.Invoke();
    }
}