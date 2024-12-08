using System;
using SessionsStopwatch.ViewModels.Reminders;
using SessionsStopwatch.Views.Reminders;

namespace SessionsStopwatch.Models.Reminding;

[ReminderToViewModelBinding(typeof(AddOneTimeDeleteReminderVM))]
public class OneTimeDeleteReminder : OneTimeReminder {
    public OneTimeDeleteReminder(TimeSpan time) : base(time) {
    }

    public override void Remind() {
        base.Remind();
        
        App.RemindersManager.RemoveReminder(this);
    }
}