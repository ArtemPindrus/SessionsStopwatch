﻿using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using SessionsStopwatch.Views;
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
        Screen? primaryScreen = window.Screens.Primary;
        if (primaryScreen == null) return;
        
        PixelRect workingArea = primaryScreen.WorkingArea;

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