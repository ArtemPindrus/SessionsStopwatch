using System;
using Avalonia.Controls;
using MsBox.Avalonia;
using SessionsStopwatch.Utilities;

namespace SessionsStopwatch.Views;

public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();
        this.BoundToScreen();
        this.RegisterDragWindow(this);
    }
}