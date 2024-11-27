using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SessionsStopwatch.Models.Reminding;
using SessionsStopwatch.ViewModels.Reminders;
using SessionsStopwatch.Views;

namespace SessionsStopwatch.ViewModels;

public partial class SettingsWindowViewModel : ViewModelBase {
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AddReminderViewModel))]
    private Type? reminderTypeSelector;

    public ViewModelBase AddReminderViewModel {
        get {
            if (ReminderTypeSelector == typeof(OneTimeReminder)) return new AddOneTimeReminderVM();
            else throw new NotSupportedException();
        }
    }
    
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

    private void AppSettingsOnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
        OnPropertyChanged(e.PropertyName);
    }

    [RelayCommand]
    private void CloseWindow() {
        var lifetime = (IClassicDesktopStyleApplicationLifetime)App.Current.ApplicationLifetime;
        
        lifetime.Windows.First(x => x.GetType() == typeof(SettingsWindow)).Close();
    }
}