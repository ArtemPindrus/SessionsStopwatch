using System;
using System.Text.Json.Serialization;
using SessionsStopwatch.Views;
using SessionsStopwatch.ViewModels;
using SessionsStopwatch.ViewModels.Reminders;
using SessionsStopwatch.Views.Reminders;

namespace SessionsStopwatch.Models.Reminding;

[ReminderToViewModelBinding(typeof(AddOneTimeReminderVM))]
public class OneTimeReminder : Reminder {
    private bool remindedOnce;
    
    [JsonConstructor]
    public OneTimeReminder(TimeSpan time) : base(time) {
    }

    public override bool CheckNeedsToRemind(TimeSpan timeElapsed) => timeElapsed >= Time && !remindedOnce;
    
    public override void Remind() {
        remindedOnce = true;

        RemindWindow window = new() {
            DataContext = new RemindWindowViewModel($"{Time.ToString()} passed!")
        };
        
        window.Show();
    }

    public override void Reset() {
        remindedOnce = false;
    }

    protected override bool EqualsTo(object obj) {
        if (obj is not OneTimeReminder other) return false;

        return Time == other.Time;
    }
}