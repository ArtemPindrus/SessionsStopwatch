using System.Windows;

namespace SessionsStopwatch.Utilities {
    /// <summary>
    /// Utility used to manage <see cref="Window"/>s.
    /// </summary>
    public static class WindowUtility {
        /// <summary>
        /// Moves <paramref name="window"/> to the bottom-right corner of the screen.
        /// </summary>
        /// <param name="window"><see cref="Window"/> to move.</param>
        public static void ToTheRightBottomCorner(Window window) {
            window.Left = SystemParameters.WorkArea.Width - window.Width;
            window.Top = SystemParameters.WorkArea.Height - window.Height;
        }

        /// <summary>
        /// Registers a <see cref="Window"/> to be handled in a way that it stands withing the screen bounds.
        /// </summary>
        /// <param name="window">Registered <see cref="Window"/>.</param>
        /// <param name="limit">Whether to limit to screen bounds. Setting to false will unregister a window.</param>
        public static void LimitToScreenBounds(this Window window, bool limit = true) {
            if (limit) ScreenBoundsLimiting.Register(window);
            else ScreenBoundsLimiting.Unregister(window);
        }

        private class ScreenBoundsLimiting {
            public static void Register(Window window) {
                window.LocationChanged += HandleWindowLocationChanged;
                Limit(window);
            }

            public static void Unregister(Window window) {
                window.LocationChanged -= HandleWindowLocationChanged;
            }

            private static void HandleWindowLocationChanged(object? sender, EventArgs e) {
                if (sender is Window window) Limit(window);
            }

            private static void Limit(Window window) {
                if (window.Left < 0) window.Left = 0;
                else if (window.Left > SystemParameters.WorkArea.Width - window.Width) window.Left = SystemParameters.WorkArea.Width - window.Width;

                if (window.Top < 0) window.Top = 0;
                else if (window.Top > SystemParameters.WorkArea.Height - window.Height)
                    window.Top = SystemParameters.WorkArea.Height - window.Height;
            }
        }
    }
}