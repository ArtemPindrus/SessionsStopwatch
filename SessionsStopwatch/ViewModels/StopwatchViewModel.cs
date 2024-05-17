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
#pragma warning restore CA1822 // Mark members as static

        public StopwatchViewModel() {
            AppStopwatch.TimeElapsedChanged += AppStopwatch_TimeElapsedChanged;

            RestartStopwatchCommand = new RestartStopwatchCommand();
        }

        private void AppStopwatch_TimeElapsedChanged() => NotifyPropertyChanged(nameof(ElapsedTime));

        public ICommand RestartStopwatchCommand { get; }
    }
}
