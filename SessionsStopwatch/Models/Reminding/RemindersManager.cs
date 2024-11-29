using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Avalonia.Controls.ApplicationLifetimes;
using SessionsStopwatch.Utilities;

namespace SessionsStopwatch.Models.Reminding;

public class RemindersManager {
    private const string SerializedDataPath = "reminders.json";
    
    [JsonInclude]
    public ObservableCollection<Reminder> Reminders { get; private set; }
    
    private Stopwatch? stopwatch;

    private Stopwatch Stopwatch {
        get {
            if (stopwatch == null) throw new Exception("Stopwatch wasn't assigned!");
            
            return stopwatch;
        }
        set {
            if (stopwatch != null) {
                stopwatch.OnElapsedUpdated -= StopwatchOnElapsedUpdated;
            }

            stopwatch = value;
            stopwatch.OnElapsedUpdated += StopwatchOnElapsedUpdated;
        }
    }

    public RemindersManager() {
        Reminders = new();

        var lifetime = AppUtility.TryGetLifetimeAsClassicDesktop();
        if (lifetime != null) lifetime.Exit += LifetimeOnExit;
    }

    public RemindersManager(ObservableCollection<Reminder> reminders) {
        Reminders = reminders;
        
        var lifetime = AppUtility.TryGetLifetimeAsClassicDesktop();
        if (lifetime != null) lifetime.Exit += LifetimeOnExit;
        App.Stopwatch.PropertyChanged += StopwatchOnPropertyChanged;
    }

    public static RemindersManager TryDeserialize(Stopwatch stopwatch) {
        RemindersManager? manager = null;
        
        if (File.Exists(SerializedDataPath)) {
            string json = File.ReadAllText(SerializedDataPath);
            manager = JsonSerializer.Deserialize<RemindersManager>(json);
        }

        manager ??= new();

        manager.Stopwatch = stopwatch;
        return manager;
    }

    public void AddReminder(Reminder reminder) => Reminders.Add(reminder);

    public void RemoveReminder(Reminder reminder) => Reminders.Remove(reminder);

    public void SerializeToDefaultFile() {
        string json = JsonSerializer.Serialize<RemindersManager>(this);
        File.WriteAllText(SerializedDataPath, json);
    }

    private void ResetAll() {
        foreach (var reminder in Reminders) {
            reminder.Reset();
        }
    }
    
    private void StopwatchOnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
        if (e.PropertyName == nameof(App.Stopwatch.State)) {
            if (App.Stopwatch.State == Stopwatch.StopwatchState.Stopped) ResetAll();
        }
    }

    private void LifetimeOnExit(object? sender, ControlledApplicationLifetimeExitEventArgs e) {
        SerializeToDefaultFile();
    }
    
    private void StopwatchOnElapsedUpdated() {
        foreach (var reminder in Reminders) {
            if (reminder.CheckNeedsToRemind(Stopwatch.Elapsed)) {
                reminder.Remind();
            }
        }
    }
}