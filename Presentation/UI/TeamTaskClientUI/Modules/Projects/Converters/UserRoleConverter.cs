using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TeamTaskClient.Domain.Enums;

namespace TeamTaskClient.UI.Modules.Projects.Converters
{
    public class UserRoleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if ((int)value == (int)UserRoleEnum.EMPLOYEE)
                return "Employee";
            return "Lead";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
