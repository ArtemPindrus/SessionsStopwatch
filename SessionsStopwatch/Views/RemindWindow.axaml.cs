using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using SessionsStopwatch.Utilities;

namespace SessionsStopwatch.Views;

public partial class RemindWindow : Window {
    public RemindWindow() {
        InitializeComponent();
        this.RegisterDragWindow(this);
    }

    private void InputElement_OnKeyDown(object? sender, KeyEventArgs e) {
        if (e.Key == Key.Escape) {
            Close();
        }
    }
}