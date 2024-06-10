using SessionsStopwatch.Commands;
using SessionsStopwatch.Utilities;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace SessionsStopwatch.ViewModels {
    public class AppSettingsVM : ViewModelBase {

        public AppSettingsVM(SettingsWindow associatedWindow) {
            HideWindowCommand = new ChangeWindowVisibility(associatedWindow);
            associatedWindow.MouseRightButtonDown += HandleMouseRightButtonDown;
            DeleteReminderCommand = new DeleteReminderCommand(this);
            OpenAddReminderWindow = new OpenAddReminderWindow();
        }

        private Reminder? _lastSelected;
        public Reminder? LastSelected { 
            get => _lastSelected;
            set { 
                _lastSelected = value;
                LastSelectedChanged?.Invoke();
            }
        }
        public event Action? LastSelectedChanged;

        private void HandleMouseRightButtonDown(object sender, MouseButtonEventArgs e) {
            DependencyObject? originalSource = (DependencyObject)e.OriginalSource;

            while (originalSource != null && originalSource is not DataGridCell) {
                originalSource = VisualTreeHelper.GetParent(originalSource);
            }

            if (originalSource is DataGridCell cell) {
                DataGridRow row = DataGridRow.GetRowContainingElement(cell);

                Reminder? item = row.Item as Reminder;
                LastSelected = item;
            } else LastSelected = null;
        }

        public ICommand HideWindowCommand { get; }
        public ICommand DeleteReminderCommand { get; }
        public ICommand OpenAddReminderWindow { get; }

#pragma warning disable CA1822 // Mark members as static
        public ReadOnlyObservableCollection<Reminder> Reminders => AppStopwatch.ReminderRO;
#pragma warning restore CA1822 // Mark members as static


        public bool AutoStartOnSession {
            get => AppSettings.Default.AutoStartOnSession;
            set {
                if (AppSettings.Default.AutoStartOnSession != value) {
                    AppSettings.Default.AutoStartOnSession = value;
                    AppSettings.Default.Save();
                    NotifyPropertyChanged();
                }
            }
        }

        public bool LimitToMonitorBounds {
            get => AppSettings.Default.LimitToMonitor;
            set {
                if (AppSettings.Default.LimitToMonitor != value) {
                    AppSettings.Default.LimitToMonitor = value;
                    AppSettings.Default.Save();
                    NotifyPropertyChanged();

                    if (value == true) WindowUtility.LimitToScreenBounds(App.Current.MainWindow);
                }
            }
        }

        public bool Startup { 
            get => RegistryRunKeyHelper.IsInRunKey;
            set {
                if (RegistryRunKeyHelper.IsInRunKey != value) {
                    if (value == true) RegistryRunKeyHelper.AddAppToRunKey();
                    else RegistryRunKeyHelper.RemoveAppToRunKey();

                    NotifyPropertyChanged();
                }
            }
        }
    }
}
