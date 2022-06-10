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
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class PercentageToWidthConverter : IValueConverter
    {
        enum Parameters
        {
            Normal, Remainder
        }

        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture)
        {
            var castValue = (int)value;
            var direction = (Parameters)Enum.Parse(typeof(Parameters), (string)parameter ?? "Normal");
            var computedWidth = direction == Parameters.Remainder ? $"{1 - castValue / 100F}*" : $"{castValue / 100F}*";

            return computedWidth;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
