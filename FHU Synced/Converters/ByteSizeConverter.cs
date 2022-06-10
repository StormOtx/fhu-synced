using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace FHU_Synced.Converters
{
    [ValueConversion(typeof(Int64), typeof(string))]
    public class ByteSizeConverter : IValueConverter
    {
        enum Parameters
        {
            B=0, KiB, MiB, GiB
        }

        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture)
        {
            var castValue = (Int64)value;
            var rank = (double)((Parameters)Enum.Parse(typeof(Parameters), (string)parameter ?? "B"));

            return Math.Round(castValue / Math.Pow(1024, rank), 2);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
