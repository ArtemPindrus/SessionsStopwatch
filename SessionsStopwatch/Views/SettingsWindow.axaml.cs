using System;
using System.Linq;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Input;
using SessionsStopwatch.Models.Reminding;
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

    private void RemindersListOnKeyDown(object? sender, KeyEventArgs e) {
        if (e.Key == Key.X && sender is ListBox list) {
            if (list.SelectedItem is Reminder reminder) {
                App.RemindersManager.RemoveReminder(reminder);
                App.RemindersManager.SerializeToDefaultFile();
            }
        }
    }
}