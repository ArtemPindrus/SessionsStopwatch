using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SessionsStopwatch.Models.Reminding;
using SessionsStopwatch.Utilities;
using SessionsStopwatch.Views;

namespace SessionsStopwatch.ViewModels;

public partial class RemindWindowViewModel : ViewModelBase {
    private TimeSpan lastParsed;
    
    [NotifyCanExecuteChangedFor(nameof(DelayCommand))]
    [ObservableProperty]
    private string? delayTextBox;
    
    public string ReminderText { get; }
    
    public RemindWindowViewModel(string reminderText) {
        ReminderText = reminderText;
    }
    
    [RelayCommand]
    private void Close() {
        WindowUtility.CloseFirst<RemindWindow>(this);
    }

    [RelayCommand(CanExecute = nameof(CanDelay))]
    private void Delay() {
        OneTimeDeleteReminder reminder = new(App.Stopwatch.Elapsed + lastParsed) {
            Enabled = true,
            DeleteOnExit = true
        };
        App.RemindersManager.AddReminder(reminder);
        
        WindowUtility.CloseFirst<RemindWindow>(this);
    }

    private bool CanDelay() => TimeSpan.TryParse(DelayTextBox, out lastParsed);
}