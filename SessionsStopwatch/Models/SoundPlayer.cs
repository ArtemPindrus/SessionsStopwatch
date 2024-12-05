using System.ComponentModel;
using System.IO;
using LibVLCSharp.Shared;

namespace SessionsStopwatch.Models;

public static class SoundPlayer {
    private static readonly LibVLC libVLC;
    private static readonly MediaPlayer mediaPlayer;
    private static Media? media;

    static SoundPlayer() {
        libVLC = new();
        
        mediaPlayer= new(libVLC);
        UpdateMedia();
        
        App.AppSettings.PropertyChanged += AppSettingsOnPropertyChanged;
        mediaPlayer.Volume = App.AppSettings.AlarmVolume;
    }

    public static void ChangeVolume(int volume) => mediaPlayer.Volume = volume;

    public static void PlayAlarmSound() {
        if (media == null) return;
        
        // for some reason this line is required to play media multiple times
        mediaPlayer.Media = media;
        
        mediaPlayer.Play();
    }
    
    private static void AppSettingsOnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
        if (e.PropertyName == nameof(App.AppSettings.AlarmSoundPath)) {
            UpdateMedia();
        }
    }

    private static void UpdateMedia() {
        //string path = App.AppSettings.AlarmSoundPath ?? Settings.DefaultAlarmSoundPath;
        string? path = null;

        if (!string.IsNullOrEmpty(App.AppSettings.AlarmSoundPath) && File.Exists(App.AppSettings.AlarmSoundPath)) {
            path = App.AppSettings.AlarmSoundPath;
        } else if (File.Exists(Settings.DefaultAlarmSoundPath)) {
            path = Settings.DefaultAlarmSoundPath;
        }

        if (path != null) {
            media?.Dispose();
            media = new(libVLC, path);
        }

        mediaPlayer.Media = media;
    }
}