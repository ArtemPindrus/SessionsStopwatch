using System.Security.Principal;
using Microsoft.Win32.TaskScheduler;

namespace SessionsStopwatch.Models.SchedulerTasks;

public class LogonTask : TaskBase {
    public override string TaskName { get; } = "Start SessionsStopwatch";

    public override void AddTask() {
        TaskService.Instance.AddTask(TaskName, 
            new SessionStateChangeTrigger() {
                StateChange = TaskSessionStateChangeType.SessionUnlock, 
                UserId = WindowsIdentity.GetCurrent().Name
            }, 
            new ExecAction(TaskManager.ExecutableName, null, TaskManager.ExecutableDirectory)
        );
    }
}