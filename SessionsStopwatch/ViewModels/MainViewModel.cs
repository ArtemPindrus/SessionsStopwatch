using SessionsStopwatch.Commands;
using System.Windows;
using System.Windows.Input;

namespace SessionsStopwatch.ViewModels
{
    class MainViewModel : ViewModelBase {
        private readonly NavigationStore _navigationStore;


        public ViewModelBase? CurrentViewModel => _navigationStore.CurrentViewModel;


        private Visibility _windowVisibility = Visibility.Visible;
        public Visibility WindowVisibility {
            get => _windowVisibility;
            set {
                if (_windowVisibility != value) {
                    _windowVisibility = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Visibility _headerVisibility = Visibility.Hidden;
        public Visibility HeaderVisibility {
            get => _headerVisibility;
            set {
                _headerVisibility = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand OutOfTrayCommand { get; }
        public ICommand ToTrayCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand ReturnToCornerCommand { get; }
        public ICommand ChangeSettingsVisibilityCommand { get; }

        public MainViewModel(NavigationStore navigationStore, SettingsWindow settingsWindow) {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += CurrentViewModelChanged;

            OutOfTrayCommand = new TraySwitchCommand(this, false);
            ToTrayCommand = new TraySwitchCommand(this, true);
            CloseCommand = new CloseWindowCommand(Application.Current.MainWindow);
            ReturnToCornerCommand = new ReturnToCornerCommand(this);
            ChangeSettingsVisibilityCommand = new ChangeWindowVisibility(settingsWindow);
        }

        private void CurrentViewModelChanged() => NotifyPropertyChanged(nameof(CurrentViewModel));

        private void ShowHeader() {
            HeaderVisibility = Visibility.Visible;
        }

        private void HideHeader() {
            HeaderVisibility = Visibility.Hidden;
        }

        public void HandleMouseEnter(object? sender, MouseEventArgs e) {
            if (Application.Current.MainWindow != null)
                ShowHeader();
        }
        public void HandleMouseLeave(object? sender, MouseEventArgs e) {
            if (Application.Current.MainWindow != null)
                HideHeader();
        }
    }
}
