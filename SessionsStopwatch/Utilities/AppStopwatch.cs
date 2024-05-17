using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SessionsStopwatch.Utilities
{
    static class AppStopwatch {
        private static readonly DispatcherTimer _timer;

        public static TimeSpan TimeElapsed { get; private set; }
        public static event Action? TimeElapsedChanged;

        static AppStopwatch() {
            _timer = new() {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Start();

            _timer.Tick += (object? sender, EventArgs e) => {
                TimeElapsed += TimeSpan.FromSeconds(1);
                OnTimeElapsedChanged();
            };
        }

        private static void OnTimeElapsedChanged() => TimeElapsedChanged?.Invoke();

        public static void Stop() {
            _timer.Stop();
        }

        public static void Restart() {
            _timer.Start();
            TimeElapsed = TimeSpan.Zero;
            OnTimeElapsedChanged();
        }
    }
}
