using System;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SessionsStopwatch.Models.Reminding;

// TODO: describe process of adding new Reminder types.
[JsonDerivedType(typeof(OneTimeReminder), "oneTime")]
[JsonDerivedType(typeof(IntervalReminder), "interval")]
[JsonDerivedType(typeof(OneTimeDeleteReminder), "oneTimeAndDelete")]
public abstract partial class Reminder : ObservableObject {
    // TODO: play sound

    [ObservableProperty]
    private bool enabled;
    
    [ObservableProperty]
    private TimeSpan time;

    [JsonConstructor]
    protected Reminder(TimeSpan time) {
        Time = time;
    }

    public override bool Equals(object? obj) {
        if (obj == null) return false;

        return EqualsTo(obj);
    }

    public override int GetHashCode() => Time.GetHashCode();

    public abstract bool CheckNeedsToRemind(TimeSpan timeElapsed);

    public abstract void Remind();

    public abstract void Reset();

    protected abstract bool EqualsTo(object obj);

    partial void OnTimeChanged(TimeSpan value) {
        Reset();
    }
}