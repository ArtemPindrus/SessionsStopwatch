using System.Windows;

namespace SessionsStopwatch.Utilities {
    public static class WindowUtility {
        public static void ToTheRightBottomCorner(Window window) {
            window.Left = SystemParameters.WorkArea.Width - window.Width;
            window.Top = SystemParameters.WorkArea.Height - window.Height;
        }

        public static void LimitToScreenBounds(Window window) {
            if (window.Left < 0) window.Left = 0;
            else if (window.Left > SystemParameters.WorkArea.Width - window.Width) window.Left = SystemParameters.WorkArea.Width - window.Width;

            if (window.Top < 0) window.Top = 0;
            else if (window.Top > SystemParameters.WorkArea.Height - window.Height)
                window.Top = SystemParameters.WorkArea.Height - window.Height;
        }
    }
}
