using SessionsStopwatch.Commands;
using System.Windows;

namespace SessionsStopwatch.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        private Visibility _windowVisibility = Visibility.Visible;

        public ViewModelBase? CurrentViewModel => _navigationStore.CurrentViewModel;
        public Visibility WindowVisibility { 
            get => _windowVisibility;
            set {
                if (_windowVisibility != value) {
                    _windowVisibility = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public OutOfTrayCommand OutOfTrayCommand { get; }
        public ToTrayCommand ToTrayCommand { get; }
        public CloseCommand CloseCommand { get; }
        public ReturnToCornerCommand ReturnToCornerCommand { get; }

        public MainViewModel(NavigationStore navigationStore) {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += CurrentViewModelChanged;

            OutOfTrayCommand = new(this);
            ToTrayCommand = new(this);
            CloseCommand = new();
            ReturnToCornerCommand = new(this);
        }

        private void CurrentViewModelChanged() => NotifyPropertyChanged(nameof(CurrentViewModel));
    }
}
