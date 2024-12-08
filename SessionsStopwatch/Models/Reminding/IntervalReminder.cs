using System;
using System.Text.Json.Serialization;
using SessionsStopwatch.ViewModels;
using SessionsStopwatch.ViewModels.Reminders;
using SessionsStopwatch.Views;

namespace SessionsStopwatch.Models.Reminding;

[ReminderToViewModelBinding(typeof(AddIntervalReminderVM))]
public class IntervalReminder : Reminder {
    [JsonIgnore]
    private int remindedCount;
    
    public IntervalReminder(TimeSpan time) : base(time) {
        
    }

    public override void Remind() {
        RemindWindow remindWindow = new() {
            DataContext = new RemindWindowViewModel($"{Time} passed! ({remindedCount})")
        };
        
        remindWindow.Show();
    }

    public override bool CheckNeedsToRemind(TimeSpan timeElapsed) {
        int remind = (int)(timeElapsed / Time);

        if (remind > remindedCount) {
            remindedCount++;
            return true;
        }

        return false;
    }

    public override void Reset() {
        remindedCount = 0;
    }

    protected override bool EqualsTo(object obj) {
        if (obj is not IntervalReminder other) return false;

        return Time == other.Time;
    }
}