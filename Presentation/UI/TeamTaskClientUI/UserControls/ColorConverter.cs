﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TeamTaskClient.UI.UserControls
{
    internal class ColorConverter : IValueConverter
    {
        Dictionary<int, Brush> _colors = new Dictionary<int, Brush>()
        {
            {0 , Brushes.Red},
            {1 , Brushes.Purple},
            {2 , Brushes.Orange},
            {3 , Brushes.Blue},
            {4 , Brushes.Violet}
        };


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return _colors.GetValueOrDefault((int)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
