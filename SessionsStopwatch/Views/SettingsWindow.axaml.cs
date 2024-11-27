using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SessionsStopwatch.Utilities;

namespace SessionsStopwatch.Views;

public partial class SettingsWindow : Window {
    public SettingsWindow() {
        InitializeComponent();
        this.RegisterDragWindow(this);

        Type baseType = typeof(Reminder);

        var items = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => x != baseType && x.IsAssignableTo(typeof(Reminder)))
            .ToArray();
        AddReminderTypeSelector.ItemsSource = items;
    }
    }
}