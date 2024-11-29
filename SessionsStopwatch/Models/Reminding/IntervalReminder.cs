using System;
using System.Text.Json.Serialization;
using SessionsStopwatch.ViewModels;
using SessionsStopwatch.Views;

namespace SessionsStopwatch.Models.Reminding;

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
}