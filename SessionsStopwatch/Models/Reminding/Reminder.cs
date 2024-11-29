using System;
using System.Text.Json.Serialization;
using SessionsStopwatch.ViewModels;

namespace SessionsStopwatch.Models.Reminding;

[JsonDerivedType(typeof(Reminder), "base")]
[JsonDerivedType(typeof(OneTimeReminder), "oneTime")]
[JsonDerivedType(typeof(IntervalReminder), "interval")]
public class Reminder {
    private TimeSpan time;
    // TODO: play sound

    public TimeSpan Time {
        get => time;
        set {
            time = value;
            Reset();
        }
    }

    [JsonConstructor]
    protected Reminder(TimeSpan time) {
        Time = time;
    }

    public virtual bool CheckNeedsToRemind(TimeSpan timeElapsed) => false;

    public virtual void Remind() {
        // let derived implement
        throw new NotImplementedException();
    }

    protected virtual void Reset() {
        throw new NotImplementedException();
    }
}