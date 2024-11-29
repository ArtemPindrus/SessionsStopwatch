using System;
using CommunityToolkit.Mvvm.Input;
using SessionsStopwatch.Models.Reminding;

namespace SessionsStopwatch.ViewModels.Reminders;

public abstract partial class AddReminderBaseVM : ViewModelBase {
    public event Action? AddedReminder;
    
    protected abstract bool CanAdd();

    protected abstract Reminder CreateReminder();

    [RelayCommand(CanExecute = nameof(CanAdd))]
    private void Add() {
        Reminder reminder = CreateReminder();
        
        App.RemindersManager.AddReminder(reminder);
        AddedReminder?.Invoke();
    }
}