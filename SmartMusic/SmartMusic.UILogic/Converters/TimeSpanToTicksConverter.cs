using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace SmartMusic.UILogic.Converters
{
    public class TimeSpanToTicksConverter:IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, string culture)
        {
            var result = 0.0;
            TimeSpan temp;

            if (TimeSpan.TryParse(value.ToString(), out temp))
            {
                result = temp.Ticks;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
