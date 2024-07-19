using System.Diagnostics;

if (Process.GetProcessesByName("SessionsStopwatch").Length > 0) return;
else {
    Process.Start("SessionsStopwatch.exe");
}