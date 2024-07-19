using Microsoft.Win32.TaskScheduler;
using System.IO;
using System.Reflection;
using System.Security.Principal;

namespace SessionsStopwatch.Utilities {
    /// <summary>
    /// Utility to manage task in a TaskScheduler to start stopwatch on workstation unlock.
    /// </summary>
    public static class TaskSchedulerStartTask {
        private const string TaskName = "Start SessionsStopwatch";

        private static readonly TaskService TaskService = new();

        static TaskSchedulerStartTask() {
            if (App.Current != null) App.Current.Exit += HandleApplicationExit;
        }

        /// <summary>
        /// Invoked when registration of task is changed.
        /// </summary>
        public static event System.Action? RegistrationChanged;

        /// <summary>
        /// Gets a value indicating whether a task is existing in a TaskScheduler and has valid values.
        /// </summary>
        public static bool ExistingAndValid {
            get {
                TaskDefinition? existingTask = TaskService.FindTask(TaskName)?.Definition;

                if (existingTask != null) {
                    bool triggerValid = existingTask.Triggers[0] is SessionStateChangeTrigger logOnT
                        && logOnT.UserId == TaskTrigger.UserId
                        && logOnT.StateChange == TaskTrigger.StateChange;

                    bool actionValid = existingTask.Actions[0] is ExecAction execAction
                        && (execAction.Arguments == null || execAction.Arguments == string.Empty)
                        && execAction.Path == TaskAction.Path
                        && execAction.WorkingDirectory == TaskAction.WorkingDirectory;

                    return triggerValid && actionValid;
                } else {
                    return false;
                }
            }
        }

        private static SessionStateChangeTrigger TaskTrigger => new() { StateChange = TaskSessionStateChangeType.SessionUnlock, UserId = WindowsIdentity.GetCurrent().Name };

        private static ExecAction TaskAction {
            get {
                string assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                return new("TaskAction.exe", null, assemblyDirectory);
            }
        }

        /// <summary>
        /// Registers task in TaskScheduler. Does nothing if <see cref="ExistingAndValid"/> evaluates to true.
        /// </summary>
        public static void RegisterTask() {
            if (ExistingAndValid) return;

            TaskService.RootFolder.DeleteTask(TaskName, false);

            TaskDefinition startTask = TaskService.NewTask();

            startTask.Triggers.Add(TaskTrigger);
            startTask.Actions.Add(TaskAction);

            TaskService.RootFolder.RegisterTaskDefinition(TaskName, startTask);

            RegistrationChanged?.Invoke();
        }

        /// <summary>
        /// Deletes task from TaskScheduler. Does nothing on failure.
        /// </summary>
        public static void UnregisterTask() {
            TaskService.RootFolder.DeleteTask(TaskName, false);

            RegistrationChanged?.Invoke();
        }

        private static void HandleApplicationExit(object sender, System.Windows.ExitEventArgs e) {
            TaskService.Dispose();
        }
    }
}