using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Win32.TaskScheduler;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace SessionsStopwatch.Models.SchedulerTasks;

public static class TaskManager {
    public static readonly string ExecutableName = $"{Process.GetCurrentProcess().ProcessName}.exe";
    public static readonly string ExecutableDirectory = AppContext.BaseDirectory;
    private static readonly Dictionary<Type, TaskBase> instantiatedTasks = new();

    public static void Register<T>() where T : TaskBase, new() {
        TaskBase taskBase = GetTask<T>();
        
        Unregister(taskBase);
        
        taskBase.RegisterTask();
    }

    public static void Unregister(TaskBase taskBase) {
        Task? schedulerTask = TaskService.Instance.FindTask(taskBase.TaskName);

        schedulerTask?.Folder.DeleteTask(schedulerTask.Name);
    }

    public static void Unregister<T>() where T : TaskBase, new() {
        TaskBase taskBase = GetTask<T>();
        Task? schedulerTask = TaskService.Instance.FindTask(taskBase.TaskName);
    
        schedulerTask?.Folder.DeleteTask(schedulerTask.Name);
    }

    private static TaskBase GetTask<T>() where T : TaskBase, new() {
        Type type = typeof(T);
        
        if (!instantiatedTasks.TryGetValue(type, out TaskBase? task)) {
            task = new T();
            instantiatedTasks.Add(type, task);
        }

        return task;
    }
}