namespace SessionsStopwatch.Models.SchedulerTasks;

public abstract class TaskBase {
    public abstract string TaskName { get; }

    public abstract void AddTask();
}