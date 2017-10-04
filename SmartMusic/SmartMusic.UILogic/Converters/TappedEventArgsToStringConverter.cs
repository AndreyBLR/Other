using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace SmartMusic.UILogic.Converters
{
    public class TappedEventArgsToStringConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var stringParam = parameter as string;

            return !string.IsNullOrEmpty(stringParam) ? stringParam : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
