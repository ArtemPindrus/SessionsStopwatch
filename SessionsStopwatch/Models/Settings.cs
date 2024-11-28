using System.ComponentModel;
using System.IO;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using SessionsStopwatch.Models.SchedulerTasks;

namespace SessionsStopwatch.Models;

public partial class Settings : ObservableObject {
    private const string SettingsFileName = "settings.json";
    
    [ObservableProperty]
    private bool createStartOnLogonTask;

    [ObservableProperty]
    private bool createRestartOnLogonTask;

    public Settings() {
        PropertyChanged += OnPropertyChanged;
    }

    public static Settings TryDeserialize() {
        if (File.Exists(SettingsFileName)) {
            string jsonStr = File.ReadAllText(SettingsFileName);
            Settings? deserializedSettings = JsonSerializer.Deserialize<Settings>(jsonStr);

            if (deserializedSettings != null) {
                return deserializedSettings;
            }
        }

        return new();
    }

    public void SerializeToDefaultFile() {
        string jsonStr = JsonSerializer.Serialize(this);
        File.WriteAllText(SettingsFileName, jsonStr);
    }
    
    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
        SerializeToDefaultFile();
    }

    partial void OnCreateStartOnLogonTaskChanged(bool value) {
        if (value) TaskManager.Register<LogonTask>();
        else TaskManager.Unregister<LogonTask>();
    }

    partial void OnCreateRestartOnLogonTaskChanged(bool value) {
        if (value) TaskManager.Register<RestartOnLogonTask>();
        else TaskManager.Unregister<RestartOnLogonTask>();
    }
}