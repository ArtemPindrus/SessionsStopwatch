using SessionsStopwatch.Utilities;
using SessionsStopwatch.ViewModels;
using SessionsStopwatch.Views;
using StoreApp.Commands;
using System.ComponentModel;

namespace SessionsStopwatch.Commands {
    /// <summary>
    /// Command allows deletion of reminders from datagrid of <see cref="AppSettingsView"/>.
    /// </summary>
    public class DeleteReminderCommand : CommandBase {
        private readonly AppSettingsVM vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteReminderCommand"/> class associated with a <see cref="AppSettingsVM"/>.
        /// </summary>
        /// <param name="vm">Instance of associated <see cref="AppSettingsVM"/>.</param>
        public DeleteReminderCommand(AppSettingsVM vm) {
            this.vm = vm;
            vm.PropertyChanged += HandlePropertyChanged;
        }

        /// <summary>
        /// Deletes <see cref="AppSettingsVM.LastSelected"/> reminder of <see cref="vm"/> from the <see cref="AppStopwatch"/>.
        /// </summary>
        /// <param name="parameter"><inheritdoc/></param>
        public override void Execute(object? parameter) {
            if (vm.LastSelected != null) AppStopwatch.DeleteReminder(vm.LastSelected);
        }

        /// <inheritdoc/>
        public override bool CanExecute(object? parameter) => vm.LastSelected != null;

        private void HandlePropertyChanged(object? sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(vm.LastSelected)) OnCanExecuteChanged();
        }
    }
}