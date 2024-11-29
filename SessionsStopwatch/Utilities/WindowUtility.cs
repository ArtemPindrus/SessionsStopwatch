using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Platform;

namespace SessionsStopwatch.Utilities;

public static class WindowUtility {
    [Flags]
    public enum Edge {
        Left = 1,
        Top = 1 << 1,
        Right = 1 << 2,
        Bottom = 1 << 3,
    }

    /// <summary>
    /// Tries to get Window's frame size scaled by desktop scaling.
    /// If FrameSize isn't assigned falls back to Window's scaled width and height.
    /// </summary>
    /// <param name="window">Measured Window.</param>
    /// <returns></returns>
    public static (double width, double height) TryGetScaledFrameSize(this Window window) {
        double scaling = window.DesktopScaling;
        
        Size? frameSize = window.FrameSize;
        double width = frameSize?.Width ?? window.Width;
        double height = frameSize?.Height ?? window.Height;

        width *= scaling;
        height *= scaling;

        return (width, height);
    }
    
    public static void BoundToScreen(this Window window, bool toBound = true) {
        if (toBound) window.PointerReleased += Impl;
        else window.PointerReleased -= Impl;

        void Impl(object? sender, PointerReleasedEventArgs e) {
            // TODO: specify screen
            Screen? primaryScreen = window.Screens.Primary;
            if (primaryScreen == null) return;
            
            PixelRect screenRect = primaryScreen.WorkingArea; 

            PixelPoint currentPos = window.Position;
            PixelPoint targetPos = currentPos;

            (double width, double height) = window.TryGetScaledFrameSize();
            
            int maxXPos = screenRect.Width - (int)width;
            int maxYPos = screenRect.Height - (int)height;


            if (currentPos.X < 0) targetPos = targetPos.WithX(0);
            else if (currentPos.X > maxXPos) targetPos = targetPos.WithX(maxXPos);
            if (currentPos.Y < 0) targetPos = targetPos.WithY(0);
            else if (currentPos.Y > maxYPos) targetPos = targetPos.WithY(maxYPos);

            window.Position = targetPos;
        }
    }

    public static void RegisterDragWindow(this Window window, Control control, bool register = true) {
        if (register) control.PointerPressed += Impl;
        else control.PointerPressed -= Impl;

        void Impl(object? sender, PointerPressedEventArgs e) {
            window.BeginMoveDrag(e);
        }
    }

    public static void PlaceAtCorner(this Window window, Edge edges) {
        Screen? primaryScreen = window.Screens.Primary;
        if (primaryScreen == null) return;
        
        PixelRect workingArea = primaryScreen.WorkingArea;

        (double width, double height) = window.TryGetScaledFrameSize();

        
        PixelPoint targetPoint = new(0, 0);

        if (edges.HasFlag(Edge.Right)) {
            int targetX = workingArea.Width - (int) width;
            targetPoint = targetPoint.WithX(targetX);
        } 
        if (edges.HasFlag(Edge.Bottom)) {
            int targetY = workingArea.Height - (int) height;
            targetPoint = targetPoint.WithY(targetY);
        }
        
        window.Position = targetPoint;
    }
    
    /// <summary>
    /// Gets first Window of T type.
    /// </summary>
    /// <typeparam name="T">Window's Type.</typeparam>
    /// <returns>First found Window of T type or null if not found.</returns>
    public static T? FirstOrDefault<T>() where T : Window {
        var lifetime = AppUtility.GetLifetimeAsClassicDesktop();

        T? window = (T?)lifetime.Windows.FirstOrDefault(x => x.GetType() == typeof(T));

        return window;
    }
 
    /// <summary>
    /// Closes the first found Window of type T.
    /// </summary>
    /// <typeparam name="T">Window's Type.</typeparam>
    /// <returns>Whether Window was found and closed.</returns>
    public static bool CloseFirst<T>() where T : Window{
        Window? window = FirstOrDefault<T>();
        
        if (window != null) {
            window.Close();
            return true;
        }

        return false;
    }
}