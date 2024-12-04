using System;

namespace SessionsStopwatch.Models.Reminding;

public class OneTimeDeleteReminder : OneTimeReminder {
    public OneTimeDeleteReminder(TimeSpan time) : base(time) {
    }

    public override void Remind() {
        base.Remind();
        
        App.RemindersManager.RemoveReminder(this);
    }
}