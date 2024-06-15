using SessionsStopwatch.ViewModels;

namespace SessionsStopwatch
{
    public class NavigationStore {
        private ViewModelBase? _currentViewModel;
        public ViewModelBase? CurrentViewModel {
            get => _currentViewModel;
            set { 
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public void OnCurrentViewModelChanged() => CurrentViewModelChanged?.Invoke();

        public event Action? CurrentViewModelChanged;
    }
}
