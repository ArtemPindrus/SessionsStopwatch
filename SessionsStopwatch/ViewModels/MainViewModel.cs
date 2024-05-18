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
        private double _headerGridRowHeight = 0;
        private double _windowHeight = SizingConst.WindowHeightNoHeader;

        public double WindowHeight {
            get => _windowHeight;
            set {
                if (_windowHeight != value) {
                    _windowHeight = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double HeaderGridRowHeight {
            get => _headerGridRowHeight;
            set {
                if (_headerGridRowHeight != value) {
                    _headerGridRowHeight = value;
                    NotifyPropertyChanged();
                }
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
        public ICommand OpenSettingsCommand { get; }

        public MainViewModel(NavigationStore navigationStore) {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += CurrentViewModelChanged;

            OutOfTrayCommand = new OutOfTrayCommand(this);
            ToTrayCommand = new ToTrayCommand(this);
            CloseCommand = new CloseWindowCommand(Application.Current.MainWindow);
            ReturnToCornerCommand = new ReturnToCornerCommand(this);
            OpenSettingsCommand = new OpenSettingsCommand();
        }

        private void CurrentViewModelChanged() => NotifyPropertyChanged(nameof(CurrentViewModel));

        public void HideHeader() {
            WindowHeight = SizingConst.WindowHeightNoHeader;
            HeaderGridRowHeight = 0;
            Application.Current.MainWindow.Top += SizingConst.HeaderHeight;
        }

        public void ShowHeader() {
            WindowHeight = SizingConst.WindowHeightWithHeader;
            HeaderGridRowHeight = SizingConst.HeaderHeight;
            Application.Current.MainWindow.Top -= SizingConst.HeaderHeight;
        }

        public void OnMouseEnter(object? sender, MouseEventArgs e) => ShowHeader();
        public void OnMouseLeave(object? sender, MouseEventArgs e) {
            if (Application.Current.MainWindow != null) HideHeader();
        }
    }
}
