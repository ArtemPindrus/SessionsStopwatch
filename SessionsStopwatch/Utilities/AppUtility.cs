using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace SessionsStopwatch.Utilities;

public static class AppUtility {
    public static IClassicDesktopStyleApplicationLifetime GetLifetimeAsClassicDesktop() {
        Application? current = Application.Current;

        if (current == null) throw new Exception("Couldn't get current Application instance.");
        if (current.ApplicationLifetime == null) throw new Exception("Application doesn't have it's lifetime instantiated.");

        return (IClassicDesktopStyleApplicationLifetime)current.ApplicationLifetime;
    }
    
    public static IClassicDesktopStyleApplicationLifetime? TryGetLifetimeAsClassicDesktop() {
        Application? current = Application.Current;

        if (current == null) throw new Exception("Couldn't get current Application instance.");

        if (current.ApplicationLifetime == null) return null;

        return (IClassicDesktopStyleApplicationLifetime)current.ApplicationLifetime;
    }
}