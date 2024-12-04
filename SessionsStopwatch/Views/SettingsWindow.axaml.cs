using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml.Templates;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using SessionsStopwatch.Models.Reminding;
using SessionsStopwatch.Utilities;
using SessionsStopwatch.ViewModels.Reminders;
using SessionsStopwatch.Views.Reminders;

namespace SessionsStopwatch.Views;

public partial class SettingsWindow : Window {
    public SettingsWindow() {
        InitializeComponent();
        this.RegisterDragWindow(this);

        Assembly executingAssembly = Assembly.GetExecutingAssembly();
        Type[] types = executingAssembly.GetTypes();

        InitializeReminderTypeSelector(types);
        CheckDataTemplates(types);
    }

    private void CheckDataTemplates(Type[] assemblyTypes) {
        Type baseType = typeof(AddReminderBaseVM);
        
        foreach (var type in assemblyTypes.Where(x => x.IsAssignableTo(baseType) && x != baseType)) {
            // TODO: throw if not defined in XAML
            if (DataTemplates.Any(x => ((DataTemplate)x).DataType == type)) {
                
            }
            else {
                var messageBox = MessageBoxManager.GetMessageBoxStandard("Error!", $"DataTemplate for {type} wasn't defined in {nameof(SettingsWindow)}.axaml!");
                messageBox.ShowAsync();
            }
        }
    }

    private void InitializeReminderTypeSelector(Type[] assemblyTypes) {
        Type baseType = typeof(Reminder);

        List<Type?> items = assemblyTypes
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