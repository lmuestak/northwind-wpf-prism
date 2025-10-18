using System;
using System.Globalization;
using System.Windows.Data;

namespace Northwind.Converters
{
    public class TrimTextConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        { 
            if(value is string str)
            {
                return str.Trim();
            }
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    
}
