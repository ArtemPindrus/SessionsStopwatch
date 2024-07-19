using System.Globalization;
using System.Windows.Data;

namespace SessionsStopwatch.Utilities.Converters {
    /// <summary>
    /// Converts <see cref="AppStopwatch"/> state into <see cref="string"/> representing Tooltip text.
    /// </summary>
    internal class StopwatchStateToTooltip : IValueConverter {
        /// <summary>
        /// Converts <see cref="AppStopwatch"/> state into <see cref="string"/> representing Tooltip text.
        /// </summary>
        /// <param name="value"><inheritdoc/></param>
        /// <param name="targetType"><inheritdoc/></param>
        /// <param name="parameter"><inheritdoc/></param>
        /// <param name="culture"><inheritdoc/></param>
        /// <returns><see cref="object"/> guaranteed to be a <see cref="string"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is bool isEnabled) return isEnabled ? "Pause" : "Resume";
            else return "Fuck?";
        }

        /// <summary>
        /// Unsupported.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}