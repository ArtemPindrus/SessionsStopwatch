using System.Xml.Linq;
using SessionsStopwatch.ViewModels.Reminders;

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
}