using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;

namespace SessionsStopwatch.Models;

public class Stopwatch {
    private readonly DispatcherTimer timer;

    private TimeSpan elapsed;

    public event Action? OnElapsedUpdated;

    public TimeSpan Elapsed {
        get => elapsed;
        private set {
            elapsed = value;
            OnElapsedUpdated?.Invoke();
        }
    }
    public bool IsEnabled { get; private set; }

    public Stopwatch() {
        timer = new() {
            Interval = TimeSpan.FromSeconds(1)
        };
        timer.Tick += OnSecondPassed;
    }

    public void Resume() {
        timer.Start();
        IsEnabled = true;
    }

    public void Pause() {
        timer.Stop();
        IsEnabled = false;
    }

    public void Stop() {
        timer.Stop();
        Elapsed = TimeSpan.Zero;
        IsEnabled = false;
    }

    private void OnSecondPassed(object? sender, EventArgs e) {
        Elapsed += TimeSpan.FromSeconds(1);
    }
}