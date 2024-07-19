using System.Globalization;
using System.Windows.Data;

namespace SessionsStopwatch.Utilities.Converters {
    /// <summary>
    /// Converts state of <see cref="AppStopwatch"/> to <see cref="string"/> representing image source.
    /// </summary>
    internal class StopwatchStateToImageSource : IValueConverter {
        /// <summary>
        /// Converts state of <see cref="AppStopwatch"/> to <see cref="string"/> representing image source.
        /// </summary>
        /// <param name="value"><inheritdoc/></param>
        /// <param name="targetType"><inheritdoc/></param>
        /// <param name="parameter"><inheritdoc/></param>
        /// <param name="culture"><inheritdoc/></param>
        /// <returns><see cref="object"/> that is guaranteed to be a <see cref="string"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is bool isEnabled) return isEnabled ? @"\Icons\Pause.png" : @"\Icons\Play.ico";
            else return "Unknown";
        }

        /// <summary>
        /// Isn't supported.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}