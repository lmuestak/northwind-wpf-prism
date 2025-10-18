using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Northwind.Converters
{

    public enum AvatarType
    {
        Original,
        Modern,
        Avatars,
    }

    public class NameToImageConverter : IValueConverter
    {
        public AvatarType ImageSource { get; set; } = AvatarType.Avatars;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string name && !string.IsNullOrWhiteSpace(name))
            {
                switch(ImageSource)
                {
                    case AvatarType.Original:
                        return new BitmapImage(new Uri($"pack://application:,,,/Northwind;component/Resources/Images/Original/{name.ToLowerInvariant()}.png"));
                    case AvatarType.Modern:
                        return new BitmapImage(new Uri($"pack://application:,,,/Northwind;component/Resources/Images/Modern/{name.ToLowerInvariant()}.jpeg"));
                    case AvatarType.Avatars:
                        return new BitmapImage(new Uri($"pack://application:,,,/Northwind;component/Resources/Images/Avatars/{name.ToLowerInvariant()}.png"));
                }
                return new BitmapImage(new Uri($"pack://application:,,,/Northwind;component/Resources/Images/Modern/{name.ToLowerInvariant()}.jpeg"));
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
