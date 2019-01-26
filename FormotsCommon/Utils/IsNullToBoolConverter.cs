using System;
using System.Globalization;
using System.Windows.Data;

namespace FormotsCommon.Utils
{
    public class IsNullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Two-way binding not supported by IsNullToBoolConverter");
        }
    }
}
