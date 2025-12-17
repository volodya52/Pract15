using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Pract15.Conventor
{
    public class BorderConventer:IValueConverter
    {
        public object Convert (object value, Type targetType, object parameter, CultureInfo culture) 
        {
            int count = (int) value;
            if (count <= 10) return Brushes.Yellow;
            return Brushes.Black;
              
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException( );
        }
    }
}
