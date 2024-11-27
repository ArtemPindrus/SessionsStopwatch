using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Tmds.DBus.Protocol;

namespace SessionsStopwatch.Utilities;

public static class WindowUtility {
    [Flags]
    public enum Edge {
        Left = 1,
        Top = 1 << 1,
        Right = 1 << 2,
        Bottom = 1 << 3,
    }
    
    public static void BoundToScreen(this Window window, bool toBound = true) {
        if (toBound) window.PointerReleased += Impl;
        else window.PointerReleased -= Impl;

        void Impl(object? sender, PointerReleasedEventArgs e) {
            PixelRect screenRect = window.Screens.Primary.WorkingArea; // TODO: get the screen the app is on

            PixelPoint currentPos = window.Position;
            PixelPoint targetPos = currentPos;

            double scaling = window.DesktopScaling;
            int maxXPos = screenRect.Width - (int)(window.FrameSize.Value.Width * scaling);
            int maxYPos = screenRect.Height - (int)(window.FrameSize.Value.Height * scaling);


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
        PixelRect workingArea = window.Screens.Primary.WorkingArea;
        Size frameSize = window.FrameSize.Value;

        double width = frameSize.Width * window.DesktopScaling;
        double height = frameSize.Height * window.DesktopScaling;

        
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

    public static bool CloseFirst<T>() where T : Window{
        var lifetime = AppUtility.GetLifetimeAsClassicDesktop();

        Window? window = lifetime.Windows.FirstOrDefault(x => x.GetType() == typeof(T));
        
        if (window != null) {
            window.Close();
            return true;
        }

        return false;
    }
}