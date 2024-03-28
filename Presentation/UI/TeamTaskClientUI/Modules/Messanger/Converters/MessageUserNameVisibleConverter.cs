using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.UI.Modules.Messanger.Converters
{
    internal class MessageUserNameVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return null;

            if (((MessageEntity)value).ID == int.Parse( ConfigurationManager.AppSettings["userId"]))
            {
                return Visibility.Hidden;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
