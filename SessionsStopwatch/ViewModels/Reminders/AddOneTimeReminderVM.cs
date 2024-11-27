using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SessionsStopwatch.Models.Reminding;

namespace SessionsStopwatch.ViewModels.Reminders;

public partial class AddOneTimeReminderVM : ViewModelBase {
    private TimeSpan lastParsedTime;
    
    [NotifyCanExecuteChangedFor(nameof(AddCommand))]
    [ObservableProperty] 
    private string? timeTextBox;
    
    [RelayCommand(CanExecute = nameof(CanAdd))]
    private void Add() {
        OneTimeReminder reminder = new(lastParsedTime);
        
        App.RemindersManager.AddReminder(reminder);
    }

    private bool CanAdd() {
        return TimeSpan.TryParse(TimeTextBox, out lastParsedTime);
    }
}