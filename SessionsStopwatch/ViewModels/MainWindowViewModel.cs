using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SessionsStopwatch.Views;

namespace SessionsStopwatch.ViewModels;

public partial class MainWindowViewModel : ViewModelBase {
    [ObservableProperty] 
    private bool headerVisibility;
    
    [ObservableProperty]
    private ViewModelBase currentViewModel = new StopwatchViewModel();
    
    [ObservableProperty]
    private bool windowIsVisible = true;

    public void ShowHeader(bool state) {
        HeaderVisibility = state;
    }

    [RelayCommand]
    private void OpenSettingsWindow() {
        var lifetime = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
        
        if (lifetime.Windows.Any(x => x.GetType() == typeof(SettingsWindow))) return;
        
        SettingsWindow window = new() {
            DataContext = new SettingsWindowViewModel()
        };
            
        window.Show();
    }
    
    [RelayCommand]
    private void Close() {
        IClassicDesktopStyleApplicationLifetime lifetime = (IClassicDesktopStyleApplicationLifetime)App.Current.ApplicationLifetime;
        lifetime.Shutdown();
    }

    [RelayCommand]
    private void ToTray() {
        WindowIsVisible = false;
    }

    [RelayCommand]
    private void Show() {
        WindowIsVisible = true;
    }
}