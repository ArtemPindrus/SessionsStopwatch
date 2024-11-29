using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SessionsStopwatch.Models.Reminding;
using SessionsStopwatch.Utilities;
using SessionsStopwatch.ViewModels.Reminders;
using SessionsStopwatch.Views;
using SessionsStopwatch.Views.Reminders;

namespace SessionsStopwatch.ViewModels;

public partial class SettingsWindowViewModel : ViewModelBase {
    [ObservableProperty]
    private Type? reminderTypeSelector;

    [ObservableProperty] 
    private AddReminderBaseVM? addReminderViewModel;
    
    public bool CreateStartOnLogonTask {
        get => App.AppSettings.CreateStartOnLogonTask;
        set => App.AppSettings.CreateStartOnLogonTask = value;
    }
    
    public bool CreateRestartOnLogonTask {
        get => App.AppSettings.CreateRestartOnLogonTask;
        set => App.AppSettings.CreateRestartOnLogonTask = value;
    }

    public ObservableCollection<Reminder> Reminders => App.RemindersManager.Reminders;

    public SettingsWindowViewModel() {
        App.AppSettings.PropertyChanged += AppSettingsOnPropertyChanged;
    }

    partial void OnReminderTypeSelectorChanged(Type? value) {
        if (AddReminderViewModel != null) AddReminderViewModel.AddedReminder -= OnAddedReminder;

        if (value == null) AddReminderViewModel = null;
        else if (value == typeof(OneTimeReminder)) AddReminderViewModel = new AddOneTimeReminderVM();
        else if (value == typeof(IntervalReminder)) AddReminderViewModel = new AddIntervalReminderVM();
        else throw new NotImplementedException();
        
        if (AddReminderViewModel != null) AddReminderViewModel.AddedReminder += OnAddedReminder;
    }

    private void OnAddedReminder() {
        ReminderTypeSelector = null;
    }

    private void AppSettingsOnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
        OnPropertyChanged(e.PropertyName);
    }

    [RelayCommand]
    private void CloseWindow() {
        WindowUtility.CloseFirst<SettingsWindow>();
    }
}