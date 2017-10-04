using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
using SmartMusic.Core.DataModel;
using SmartMusic.Core.DataModel.MusicCollection;
using SmartMusic.Core.Repositories;

namespace SmartMusic.UILogic.Converters
{
    public class PathToTrackConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            var path = value.ToString();

            try
            {
                //var track = MusicRepository.Instance.GetTrackByPath(path);
                //if (track == null)
                //{
                //    track = new Track
                //                {
                //                    Title = Path.GetFileName(path)
                //                };
                //}
                //else
                //{
                //    track.Title = Path.GetFileName(path);
                //}

                //return track;
                return null;
            }
            catch
            {
                return new BitmapImage();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }

    }
}
