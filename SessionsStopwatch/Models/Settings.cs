using System.IO;
using System.Text.Json;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using SessionsStopwatch.Utilities;

namespace SessionsStopwatch.Models;

public partial class Settings : ObservableObject {
    private const string SettingsFileName = "settings.json";
    
    [ObservableProperty]
    private bool createLogonTask;

    public static Settings TryDeserialize() {
        var lifetime = AppUtility.TryGetLifetimeAsClassicDesktop();

        var settings = GetInstance();
        if (lifetime != null) lifetime.Exit += settings.LifetimeOnExit;

        return settings;

        Settings GetInstance() {
            if (File.Exists(SettingsFileName)) {
                string jsonStr = File.ReadAllText(SettingsFileName);
                Settings? deserializedSettings = JsonSerializer.Deserialize<Settings>(jsonStr);

                if (deserializedSettings != null) {
                    return deserializedSettings;
                }
            }

            return new();
        }
    }

    private void LifetimeOnExit(object? sender, ControlledApplicationLifetimeExitEventArgs e) {
        //TODO: serialize
        using StreamWriter writer = new(SettingsFileName);
        
        string jsonStr = JsonSerializer.Serialize(this);
        writer.Write(jsonStr);
    }

    partial void OnCreateStartOnLogonTaskChanged(bool value) {
        if (value) TaskManager.Register<LogonTask>();
        else TaskManager.Unregister<LogonTask>();
    }
    }
}