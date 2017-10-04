using System;
using System.Globalization;
using System.Windows.Data;

namespace MSExchangeClient.Infrastructure.Converters
{
    public class Bool2SizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b;
            double s;
            if (value == null || parameter == null) return value;
            if (!bool.TryParse(value.ToString(), out b) || !double.TryParse(parameter.ToString(), out s)) return value;

            return b ? s : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }  
    }
}
