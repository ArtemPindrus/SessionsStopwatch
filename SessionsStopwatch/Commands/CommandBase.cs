using System.Windows.Input;

namespace StoreApp.Commands {
    /// <summary>
    /// Defines base class for all Commands to inherit.
    /// </summary>
    public abstract class CommandBase : ICommand {
        /// <inheritdoc/>
        public event EventHandler? CanExecuteChanged;

        /// <inheritdoc/>
        public virtual bool CanExecute(object? parameter) => true;

        /// <inheritdoc/>
        public abstract void Execute(object? parameter);

        /// <summary>
        /// Provides a concise way to invoke <see cref="CanExecuteChanged"/> event.
        /// </summary>
        protected void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}