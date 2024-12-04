using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExCSS;
using SessionsStopwatch.Models.Reminding;
using SessionsStopwatch.Views.Reminders;

namespace SessionsStopwatch.ViewModels.Reminders;

public partial class AddIntervalReminderVM : AddReminderBaseVM {
    private TimeSpan lastParsed;
    
    [NotifyCanExecuteChangedFor(nameof(AddCommand))]
    [ObservableProperty]
    private string? intervalTextBox;

    protected override Reminder CreateReminder() => new IntervalReminder(lastParsed);

    protected override bool CanAdd() {
        return TimeSpan.TryParse(IntervalTextBox, out lastParsed);
    }
}