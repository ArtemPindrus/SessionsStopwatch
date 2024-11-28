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

        List<Type?> items = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => x != baseType && x.IsAssignableTo(typeof(Reminder)))
            .ToList<Type?>();
        items.Add(null);
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