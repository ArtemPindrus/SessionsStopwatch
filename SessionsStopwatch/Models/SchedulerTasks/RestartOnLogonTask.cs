using System.Security.Principal;
using Microsoft.Win32.TaskScheduler;

namespace SessionsStopwatch.Models.SchedulerTasks;

public class RestartOnLogonTask : TaskBase {
    private const string RestartStopwatchTaskExe = "RestartStopwatchTask.exe";
    public override string TaskName { get; } = "Restart SessionStopwatch";
    
    public override void AddTask() {
        TaskService.Instance.AddTask(TaskName,
            new SessionStateChangeTrigger() {
                StateChange = TaskSessionStateChangeType.SessionUnlock, 
                UserId = WindowsIdentity.GetCurrent().Name
            }, 
            new ExecAction(RestartStopwatchTaskExe, null, TaskManager.ExecutableDirectory)
        );
        
        
    }
}