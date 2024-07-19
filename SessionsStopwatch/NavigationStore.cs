using SessionsStopwatch.ViewModels;

namespace SessionsStopwatch {
    /// <summary>
    /// Stores ViewModel, View of which should be displayed in the MainWindow.
    /// </summary>
    public class NavigationStore {
        private ViewModelBase? currentViewModel;

        /// <summary>
        /// Gets invoked when the CurrentViewModel changes
        /// </summary>
        public event Action? CurrentViewModelChanged;

        /// <summary>
        /// Gets or sets ViewModel of the View that should be displayed in the MainWindow.
        /// </summary>
        public ViewModelBase? CurrentViewModel {
            get => currentViewModel;
            set {
                currentViewModel = value;
                CurrentViewModelChanged?.Invoke();
            }
        }
    }
}