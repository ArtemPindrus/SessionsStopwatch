using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SessionsStopwatch.Utilities.Converters
{
    class StopwatchStateToImageSource : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is bool isEnabled) return isEnabled ? @"\Icons\Pause.png" : @"\Icons\Play.ico";
            else return "Unknown";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
