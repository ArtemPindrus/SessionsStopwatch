using SessionsStopwatch.Utilities;
using SessionsStopwatch.ViewModels;
using StoreApp.Commands;

namespace SessionsStopwatch.Commands
{
    class DeleteReminderCommand : CommandBase {
        private readonly AppSettingsVM _vm;

        public DeleteReminderCommand(AppSettingsVM vm) { 
            _vm = vm;
            vm.LastSelectedChanged += OnCanExecuteChanged;
        }

        public override void Execute(object? parameter) {
            if (_vm.LastSelected != null) AppStopwatch.DeleteReminder(_vm.LastSelected);
        }

        public override bool CanExecute(object? parameter) => _vm.LastSelected != null;
    }
}
