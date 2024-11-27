using System;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SessionsStopwatch.Models;

public partial class Stopwatch : ObservableObject {
    public enum StopwatchState {
        Stopped,
        Running,
        Paused
    }
    
    private readonly DispatcherTimer timer;

    private TimeSpan elapsed;

    [ObservableProperty]
    private StopwatchState state = StopwatchState.Stopped;

    public event Action? OnElapsedUpdated;

    public TimeSpan Elapsed {
        get => elapsed;
        private set {
            elapsed = value;
            OnElapsedUpdated?.Invoke();
        }
    }

    public Stopwatch() {
        timer = new() {
            Interval = TimeSpan.FromSeconds(1)
        };
        timer.Tick += OnSecondPassed;
    }

    public void Resume() {
        timer.Start();

        State = StopwatchState.Running;
    }

    public void Pause() {
        timer.Stop();
        
        State = StopwatchState.Paused;
    }

    public void Stop() {
        timer.Stop();
        Elapsed = TimeSpan.Zero;
        
        State = StopwatchState.Stopped;
    }

    private void OnSecondPassed(object? sender, EventArgs e) {
        Elapsed += TimeSpan.FromSeconds(1);
    }
}