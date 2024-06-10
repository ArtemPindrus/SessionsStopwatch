using System.Globalization;
using System.Windows.Data;

namespace SessionsStopwatch.Utilities.Converters
{
    class StopwatchStateToTooltip : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is bool isEnabled) return isEnabled ? "Pause" : "Resume";

            else return "Fuck?";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
