using System.Security.Principal;
using Microsoft.Win32.TaskScheduler;

namespace SessionsStopwatch.Models.SchedulerTasks;

public class LogonTask : TaskBase {
    public override string TaskName { get; } = "Start SessionsStopwatch";

    protected override TaskDefinition CreateTaskDefinition() {
        var taskDefinition = TaskService.Instance.NewTask();
        taskDefinition.Triggers.Add(new SessionStateChangeTrigger() {
            StateChange = TaskSessionStateChangeType.SessionUnlock,
            UserId = WindowsIdentity.GetCurrent().Name
        });
        taskDefinition.Actions.Add(new ExecAction(TaskManager.ExecutableName, null, TaskManager.ExecutableDirectory));
        
        taskDefinition.Settings.DisallowStartIfOnBatteries = false;
        taskDefinition.Settings.StopIfGoingOnBatteries = false;

        return taskDefinition;
    }
}