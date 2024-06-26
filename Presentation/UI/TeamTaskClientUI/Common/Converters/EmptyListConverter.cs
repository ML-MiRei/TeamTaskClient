﻿using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TeamTaskClient.UI.Common.Converters
{
    class EmptyListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;

            if (((ICollection)value).Count == 0)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
