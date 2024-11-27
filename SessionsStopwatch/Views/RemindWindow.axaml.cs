using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SessionsStopwatch.Utilities;

namespace SessionsStopwatch.Views;

public partial class RemindWindow : Window {
    public RemindWindow() {
        InitializeComponent();
        this.RegisterDragWindow(this);
    }
}