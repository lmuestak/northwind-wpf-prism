using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Northwind.Converters
{
    public class NameToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string name && !string.IsNullOrWhiteSpace(name))
            {
                return new BitmapImage(new Uri($"pack://application:,,,/Northwind;component/Resources/Images/{name.ToLowerInvariant()}.jpeg"));
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
