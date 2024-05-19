using Microsoft.Win32;

namespace SessionsStopwatch.Utilities
{
    public static class RegistryRunKeyHelper {
        private const string RunKeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        private const string AppName = "SessionStopwatch";

        private static string AppPath => System.Reflection.Assembly.GetExecutingAssembly().Location;
        public static bool IsInRunKey {
            get {
                RegistryKey? key = Registry.CurrentUser.OpenSubKey(RunKeyPath, true);

                return key != null && key.GetValue(AppName) is string value && value == AppPath;
            }
        }

        public static void AddAppToRunKey() { 
            RegistryKey? key = Registry.CurrentUser.OpenSubKey(RunKeyPath, true);

            key?.SetValue(AppName, AppPath);
        }

        public static void RemoveAppToRunKey() {
            RegistryKey? key = Registry.CurrentUser.OpenSubKey(RunKeyPath, true);


            key?.DeleteValue(AppName, true);
        }
    }
}
