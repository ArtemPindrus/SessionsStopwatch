namespace SessionsStopwatch.Models.SchedulerTasks;

public abstract class TaskBase {
    public abstract string TaskName { get; }
    
    public void RegisterTask() {
        TaskService.Instance.RootFolder.RegisterTaskDefinition(TaskName, CreateTaskDefinition());
    }
    
    protected abstract TaskDefinition CreateTaskDefinition();
}