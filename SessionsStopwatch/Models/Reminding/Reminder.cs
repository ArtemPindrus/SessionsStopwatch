using System;
using System.Text.Json.Serialization;
using SessionsStopwatch.ViewModels;

namespace SessionsStopwatch.Models.Reminding;

[JsonDerivedType(typeof(Reminder), "base")]
[JsonDerivedType(typeof(OneTimeReminder), "oneTime")]
public class Reminder {
    private TimeSpan time;

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

    public virtual void Reset() {
        throw new NotImplementedException();
    }
}