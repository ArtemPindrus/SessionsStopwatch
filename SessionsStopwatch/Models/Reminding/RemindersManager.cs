using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using Avalonia.Controls.ApplicationLifetimes;
using SessionsStopwatch.Utilities;

namespace SessionsStopwatch.Models.Reminding;

public class RemindersManager {
    private static readonly Lock collectionLock = new();
    private const string SerializedDataPath = "reminders.json";

    private Stopwatch? stopwatch;
    private ObservableCollection<Reminder> reminders;
    
    [JsonInclude]
    public ObservableCollection<Reminder> Reminders {
        get {
            lock (collectionLock) {
                return reminders;
            }
        }
        private set {
            lock (collectionLock) {
                reminders = value;
            }
        }
    }

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

    public bool AddReminder(Reminder reminder) {
        lock (collectionLock) {
            if (Reminders.Any(x => x.Equals(reminder))) return false;
        
            Reminders.Add(reminder);
        
            SerializeToDefaultFile();
        }
        
        return true;
    }

    public void RemoveReminder(Reminder reminder) {
        lock (collectionLock) {
            Reminders.Remove(reminder);

            SerializeToDefaultFile();
        }
    }

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
        DeleteAllOnExit();
        SerializeToDefaultFile();
    }

    private void DeleteAllOnExit() {
        for (int i = 0; i < Reminders.Count; i++) {
            Reminder reminder = Reminders[i];
            if (reminder.DeleteOnExit) {
                Reminders.RemoveAt(i);
                i--;
            }
        }
    }
    
    private void StopwatchOnElapsedUpdated() {
        lock (collectionLock) {
            int length = Reminders.Count;
            for (int i = 0; i < Reminders.Count; i++) {
                Reminder reminder = Reminders[i];
                if (!reminder.Enabled) return;
                
                if (reminder.CheckNeedsToRemind(Stopwatch.Elapsed)) {
                    reminder.Remind();

                    // handle reminder deletion
                    int newLength = Reminders.Count;
                    if (length > newLength) {
                        length = newLength;
                        i--;
                    }
                }
            }
        }
    }
}