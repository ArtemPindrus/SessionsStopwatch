using System;
using SessionsStopwatch.ViewModels.Reminders;

namespace SessionsStopwatch.Models.Reminding;

[AttributeUsage(AttributeTargets.Class)]
public class ReminderToViewModelBindingAttribute : Attribute {
    public readonly Type ViewModel;

    public ReminderToViewModelBindingAttribute(Type viewModel) {
        ArgumentNullException.ThrowIfNull(viewModel);
        if (!viewModel.IsAssignableTo(typeof(AddReminderBaseVM))) 
            throw new ArgumentException($"Provided type isn't {nameof(AddReminderBaseVM)}.", nameof(viewModel));
        
        ViewModel = viewModel;
    }
}