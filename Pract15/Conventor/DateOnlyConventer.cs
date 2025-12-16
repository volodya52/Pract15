using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Pract15.Conventor
{
    public class DateOnlyConventer:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if(value is DateOnly dateOnly)
                {
                    return dateOnly.ToDateTime(TimeOnly.MinValue);
                }
                return null;
                
            }
            catch
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetTypem, object parameter, CultureInfo culture)
        {
            try
            {
                if(value is DateTime dateTime)
                {
                    return DateOnly.FromDateTime(dateTime);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
