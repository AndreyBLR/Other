using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace SmartMusic.UILogic.Converters
{
    public class TimeSpanToStringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            string result = string.Empty;
            var temp = TimeSpan.Zero;

            if (TimeSpan.TryParse(value.ToString(), out temp))
            {
                string formatForMinutes = "{0}";
                string formatForSeconds = "{1}";

                if (temp.Minutes < 10)
                    formatForMinutes = "0{0}";
                if (temp.Seconds < 10)
                    formatForSeconds = "0{1}";

                result = string.Format(string.Format("{0}:{1}", formatForMinutes, formatForSeconds), temp.Minutes, temp.Seconds);
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
