using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using SmartMusic.Core.DataModel;
using SmartMusic.Core.DataModel.MusicCollection;

namespace SmartMusic.UILogic.Converters
{
    public class TappedEventArgsToMusicCollectionEntityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter is Artist)
            {
                return (Artist) parameter;
            }
            if (parameter is Album)
            {
                return (Album)parameter;
            }
            if (parameter is Track)
            {
                return (Track) parameter;
            }
            if (parameter is Track)
            {
                return (Track) parameter;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
