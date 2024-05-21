using SessionsStopwatch.Commands;
using SessionsStopwatch.Utilities;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace SessionsStopwatch.ViewModels
{
    class MainViewModel : ViewModelBase {
        private readonly NavigationStore _navigationStore;

        private Visibility _windowVisibility = Visibility.Visible;

        private double _windowHeight = SizingConst.WindowHeightNoHeader;
        public double WindowHeight {
            get => _windowHeight;
            set {
                _windowHeight = value;
                NotifyPropertyChanged();
            }
        }

        private double _headerRowHeight = 0;
        public double HeaderRowHeight {
            get => _headerRowHeight;
            set {
                _headerRowHeight = value;
                NotifyPropertyChanged();
            }
        }

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
            WindowHeight = SizingConst.WindowHeightWithHeader;
            Application.Current.MainWindow.Top -= SizingConst.HeaderHeight;
            HeaderRowHeight = SizingConst.HeaderHeight;
        }

        private void HideHeader() {
            WindowHeight = SizingConst.WindowHeightNoHeader;
            Application.Current.MainWindow.Top += SizingConst.HeaderHeight;
            HeaderRowHeight = 0;
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
