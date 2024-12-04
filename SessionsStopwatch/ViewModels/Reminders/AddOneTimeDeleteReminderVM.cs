using System;
using CommunityToolkit.Mvvm.ComponentModel;
using SessionsStopwatch.Models.Reminding;

namespace SessionsStopwatch.ViewModels.Reminders;

public partial class AddOneTimeDeleteReminderVM : AddReminderBaseVM {
    private TimeSpan lastParsedTime;
    
    [NotifyCanExecuteChangedFor(nameof(AddCommand))]
    [ObservableProperty] 
    private string? timeTextBox;

    protected override Reminder CreateReminder() => new OneTimeDeleteReminder(lastParsedTime);

    protected override bool CanAdd() {
        return TimeSpan.TryParse(TimeTextBox, out lastParsedTime);
    }
}