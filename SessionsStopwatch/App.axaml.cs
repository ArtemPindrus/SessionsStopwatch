using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using SessionsStopwatch.Models;
using SessionsStopwatch.Utilities;
using SessionsStopwatch.ViewModels;
using SessionsStopwatch.Views;
using Stopwatch = SessionsStopwatch.Models.Stopwatch;

namespace SessionsStopwatch;

public partial class App : Application {
    public static Settings? AppSettings;
    public static readonly Stopwatch Stopwatch = new();
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (!Design.IsDesignMode 
            && Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1) Environment.Exit(0);

        AppSettings = Settings.TryDeserialize();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);
            MainWindowViewModel mainViewModel = new();
            MainWindow mainWindow = new(){
                DataContext = mainViewModel,
            };
            
            desktop.MainWindow = mainWindow;

            mainWindow.MainContent.PointerEntered += (_, _) => mainViewModel.ShowHeader(true);
            mainWindow.PointerExited += (_, _) => mainViewModel.ShowHeader(false);

            DataContext = mainViewModel;
            
            mainWindow.PlaceAtCorner(WindowUtility.Edge.Bottom | WindowUtility.Edge.Right);
        }

        StartStopwatchRestartServer();

        base.OnFrameworkInitializationCompleted();
    }
    
    private async void StartStopwatchRestartServer() {
        while (true) {
            await using var server = new NamedPipeServerStream("RestartPipe", PipeDirection.InOut, 1, PipeTransmissionMode.Message);
            
            await server.WaitForConnectionAsync();
            using var reader = new StreamReader(server);
            
            string? message = await reader.ReadLineAsync();

            if (message == "RestartStopwatch") {
                Stopwatch.Stop();
                Stopwatch.Resume();
            }
        }
    }
}