using SessionsStopwatch.Commands;
using SessionsStopwatch.Utilities;
using System.Windows.Input;

namespace SessionsStopwatch.ViewModels
{
    class StopwatchViewModel : ViewModelBase {
#pragma warning disable CA1822 // Mark members as static
        public string ElapsedTime {
            get => AppStopwatch.TimeElapsed.ToString();
        }

        public bool IsStopwatchEnabled => AppStopwatch.IsEnabled;
#pragma warning restore CA1822 // Mark members as static

        public StopwatchViewModel() {
            AppStopwatch.TimeElapsedChanged += () => NotifyPropertyChanged(nameof(ElapsedTime)); ;
            AppStopwatch.IsEnabledChanged += () => NotifyPropertyChanged(nameof(IsStopwatchEnabled));

            RestartStopwatchCommand = new RestartStopwatchCommand();
            SwitchStopwatchState = new SwitchStopwatchState();
        }

        public ICommand SwitchStopwatchState { get; }
        public ICommand RestartStopwatchCommand { get; }
    }
}
