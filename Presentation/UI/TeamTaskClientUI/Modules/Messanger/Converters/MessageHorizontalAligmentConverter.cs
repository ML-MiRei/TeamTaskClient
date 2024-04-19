using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TeamTaskClient.UI.Modules.Messanger.Converters
{
    internal class MessageHorizontalAligmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (((string)value) == Properties.Settings.Default.userTag)
            {
                return HorizontalAlignment.Right;
            }
            return HorizontalAlignment.Left;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
