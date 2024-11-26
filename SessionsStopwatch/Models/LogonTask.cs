using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using Microsoft.Win32.TaskScheduler;
using Task = Microsoft.Win32.TaskScheduler.Task;

namespace SessionsStopwatch.Models;

public static class LogonTask {
    private const string TaskName = "Start SessionsStopwatch";
    private static readonly string ExecutableName;

    static LogonTask() {
        ExecutableName = $"{Process.GetCurrentProcess().ProcessName}.exe";
    }
    
    public static void Register() {
        Unregister();

        string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        TaskService.Instance.AddTask(TaskName, 
            new SessionStateChangeTrigger() {
                StateChange = TaskSessionStateChangeType.SessionUnlock, 
                UserId = WindowsIdentity.GetCurrent().Name
            }, 
            new ExecAction(ExecutableName, null, directory));
    }

    public static void Unregister() {
        Task? task = TaskService.Instance.FindTask(TaskName);

        task?.Folder.DeleteTask(task.Name);
    }
}