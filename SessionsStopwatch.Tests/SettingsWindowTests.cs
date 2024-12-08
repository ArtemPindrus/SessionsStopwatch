using System.Xml.Linq;
using SessionsStopwatch.Models.Reminding;
using SessionsStopwatch.Utilities;
using SessionsStopwatch.ViewModels.Reminders;
using SessionsStopwatch.Views;

namespace SessionsStopwatch.Tests;

public class SettingsWindowTests {
    private const string SettingWindowXMLPath = "SettingsWindow.axaml";
    
    [Fact]
    public void DataTemplatesForEveryAddReminderVM() {
        Type baseType = typeof(AddReminderBaseVM);
        Type[] assemblyTypes = baseType.Assembly.GetTypes();
        
        var document = XDocument.Load(SettingWindowXMLPath);

        XName name = XName.Get("Window.DataTemplates", "https://github.com/avaloniaui");
        var dataTemplates = document.Descendants(name).First().Elements();

        foreach (var type in assemblyTypes.Where(x => x.IsAssignableTo(baseType) && x != baseType)) {
            Assert.Contains(dataTemplates, (x) => x.Attribute("DataType").Value.Contains(type.Name));
        }
    }
    
    [Fact]
    public void EveryReminderBindsToViewModel() {
        foreach (var reminderType in typeof(Reminder).GetDerivedTypes()) {
            var bindings = reminderType.GetCustomAttributes(typeof(ReminderToViewModelBindingAttribute), false);
            Assert.True(bindings.Length > 0, $"Type {reminderType} doesn't bind to a {nameof(AddReminderBaseVM)}.");
        }
    }
}