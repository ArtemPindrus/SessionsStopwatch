using System.Security.Principal;
using Microsoft.Win32.TaskScheduler;

namespace SessionsStopwatch.Models.SchedulerTasks;

public class RestartOnLogonTask : TaskBase {
    private const string RestartStopwatchTaskExe = "RestartStopwatchTask.exe";
    public override string TaskName { get; } = "Restart SessionStopwatch";

    protected override TaskDefinition CreateTaskDefinition() {
        var taskDef = TaskService.Instance.NewTask();
        taskDef.Actions.Add(new ExecAction(RestartStopwatchTaskExe, null, TaskManager.ExecutableDirectory));
        taskDef.Triggers.Add(new SessionStateChangeTrigger() {
            StateChange = TaskSessionStateChangeType.SessionUnlock, 
            UserId = WindowsIdentity.GetCurrent().Name
        });
        
        taskDef.Settings.DisallowStartIfOnBatteries = false;
        taskDef.Settings.StopIfGoingOnBatteries = false;

        return taskDef;
    }
}