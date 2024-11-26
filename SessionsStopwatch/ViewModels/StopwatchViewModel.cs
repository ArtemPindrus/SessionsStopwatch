using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SessionsStopwatch.Models;

namespace SessionsStopwatch.ViewModels;

public partial class StopwatchViewModel : ViewModelBase {
    private readonly Stopwatch stopwatch;
    private readonly string resumeIcon, pauseIcon;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ChangeStateIcon))]
    private ICommand changeStateCommand;

    [ObservableProperty] 
    private string changeStateIcon;

    public string Elapsed => stopwatch.Elapsed.ToString();

    public StopwatchViewModel() {
        resumeIcon = "/Assets/Play.svg";
        pauseIcon = "/Assets/Pause.svg";

        stopwatch = App.Stopwatch;
        
        //TODO: unregister if view changes
        stopwatch.OnElapsedUpdated += OnElapsedUpdated;

        changeStateCommand = ResumeCommand;
        changeStateIcon = resumeIcon;
        
        Resume();
    }

    partial void OnChangeStateCommandChanged(ICommand value) {
        if (value == ResumeCommand) ChangeStateIcon = resumeIcon;
        else if (value == PauseCommand) ChangeStateIcon = pauseIcon;
    }

    private void OnElapsedUpdated() {
        OnPropertyChanged(nameof(Elapsed));
    }

    [RelayCommand]
    private void Pause() {
        stopwatch.Pause();
        ChangeStateCommand = ResumeCommand;
    }

    [RelayCommand]
    private void Resume() {
        stopwatch.Resume();
        ChangeStateCommand = PauseCommand;
    }

    [RelayCommand]
    private void Stop() {
        stopwatch.Stop();
        ChangeStateCommand = ResumeCommand;
    }
}