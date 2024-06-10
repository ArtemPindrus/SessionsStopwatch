using SessionsStopwatch.Utilities;
using SessionsStopwatch.ViewModels;
using StoreApp.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SessionsStopwatch.Commands
{
    class AddReminderCommand : CommandBase {
        private readonly AddAReminderVM _vm;
        private readonly Window _window;

        public AddReminderCommand(AddAReminderVM vm, Window associatedWindow) { 
            _vm = vm;
            _vm.PropertyChanged += HandlePropertyChanged;
            _window = associatedWindow;
        }

        private void HandlePropertyChanged(object? sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(_vm.SelectedBehavior)) OnCanExecuteChanged();
            else if (e.PropertyName == nameof(_vm.FullTimeBox)) OnCanExecuteChanged();
        }

        public override void Execute(object? parameter) {
            AppStopwatch.AddReminder(TimeSpan.Parse(_vm.FullTimeBox), _vm.SelectedBehavior.Value, _vm.Enabled);
            _window.Close();
        }

        public override bool CanExecute(object? parameter) {
            return _vm.SelectedBehavior != null 
                && TimeSpan.TryParse(_vm.FullTimeBox, out TimeSpan result) && result != TimeSpan.Zero 
                && (AppStopwatch.ReminderRO.Count == 0 || AppStopwatch.ReminderRO.Any(r => r.Time != result || r.Behavior != _vm.SelectedBehavior));

        }
    }
}
