using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace TeamTaskClient.UI.Modules.Messanger.Converters
{
    class EditorMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (((string)value) == Properties.Settings.Default.userTag)
            {
                return CursorType.Hand;
            }
            else
                return CursorType.Arrow;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
