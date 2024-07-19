using SessionsStopwatch.Utilities;
using SessionsStopwatch.ViewModels;
using SessionsStopwatch.Windows;
using StoreApp.Commands;
using System.ComponentModel;

namespace SessionsStopwatch.Commands {
    /// <summary>
    /// Command used to confirm adding <see cref="Reminder"/> on <see cref="AddReminderWindow"/>.
    /// </summary>
    internal class AddReminderCommand : CommandBase {
        private readonly AddReminderVM vm;
        private readonly AddReminderWindow window;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddReminderCommand"/> class associated with <see cref="AddReminderVM"/> and <see cref="AddReminderWindow"/>.
        /// </summary>
        /// <param name="vm">Associated <see cref="AddReminderVM"/>.</param>
        /// <param name="associatedWindow">Associated <see cref="AddReminderWindow"/>.</param>
        public AddReminderCommand(AddReminderVM vm, AddReminderWindow associatedWindow) {
            this.vm = vm;
            this.vm.PropertyChanged += HandlePropertyChanged;
            window = associatedWindow;
        }

        /// <summary>
        /// Adds a <see cref="Reminder"/> to <see cref="AppStopwatch"/>.
        /// </summary>
        /// <param name="parameter"><inheritdoc/></param>
        public override void Execute(object? parameter) {
            AppStopwatch.AddReminder(TimeSpan.Parse(vm.FullTimeBox), vm.SelectedBehavior!.Value, vm.Enabled);
            window.Close();
        }

        /// <inheritdoc/>
        public override bool CanExecute(object? parameter) {
            return vm.SelectedBehavior != null
                && TimeSpan.TryParse(vm.FullTimeBox, out TimeSpan result) && result != TimeSpan.Zero
                && (AppStopwatch.ReminderRO.Count == 0 || AppStopwatch.ReminderRO.Any(r => r.Time != result || r.Behavior != vm.SelectedBehavior));
        }

        private void HandlePropertyChanged(object? sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(vm.SelectedBehavior)) OnCanExecuteChanged();
            else if (e.PropertyName == nameof(vm.FullTimeBox)) OnCanExecuteChanged();
        }
    }
}