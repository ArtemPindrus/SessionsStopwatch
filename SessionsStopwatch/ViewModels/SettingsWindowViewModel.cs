using System.ComponentModel;
using System.Linq;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.Input;
using SessionsStopwatch.Models;
using SessionsStopwatch.Views;

namespace SessionsStopwatch.ViewModels;

public partial class SettingsWindowViewModel : ViewModelBase {
    public bool CreateLogonTask {
        get => App.AppSettings.CreateLogonTask;
        set => App.AppSettings.CreateLogonTask = value;
    public bool CreateRestartOnLogonTask {
        get => App.AppSettings.CreateRestartOnLogonTask;
        set => App.AppSettings.CreateRestartOnLogonTask = value;
    }

    public SettingsWindowViewModel() {
        App.AppSettings.PropertyChanged += AppSettingsOnPropertyChanged;
    }

    private void AppSettingsOnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
        if (e.PropertyName == nameof(App.AppSettings.CreateLogonTask)) {
            OnPropertyChanged(nameof(CreateLogonTask));
        }
    }

    [RelayCommand]
    private void CloseWindow() {
        var lifetime = (IClassicDesktopStyleApplicationLifetime)App.Current.ApplicationLifetime;
        
        lifetime.Windows.First(x => x.GetType() == typeof(SettingsWindow)).Close();
    }
}