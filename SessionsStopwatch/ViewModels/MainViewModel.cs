using SessionsStopwatch.Commands;
using SessionsStopwatch.Utilities;
using System.Windows;
using System.Windows.Input;

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

        public ICommand OutOfTrayCommand { get; }
        public ICommand ToTrayCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand ReturnToCornerCommand { get; }
        public ICommand OpenSettingsCommand { get; }

        public MainViewModel(NavigationStore navigationStore) {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += CurrentViewModelChanged;

            OutOfTrayCommand = new TraySwitchCommand(this, false);
            ToTrayCommand = new TraySwitchCommand(this, true);
            CloseCommand = new CloseWindowCommand(Application.Current.MainWindow);
            ReturnToCornerCommand = new ReturnToCornerCommand(this);
            OpenSettingsCommand = new OpenSettingsCommand();
        }

        private void CurrentViewModelChanged() => NotifyPropertyChanged(nameof(CurrentViewModel));
    }
}
