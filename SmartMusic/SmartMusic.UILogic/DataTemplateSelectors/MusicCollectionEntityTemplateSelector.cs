using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SmartMusic.Core.DataModel;
using SmartMusic.Core.DataModel.MusicCollection;

namespace SmartMusic.UILogic.DataTemplateSelectors
{
    public class MusicCollectionEntityTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item == null)
                return null;

            if (item is Track)
                return (DataTemplate)Application.Current.Resources["TrackDataTemplate"];
            if (item is Album)
                return (DataTemplate)Application.Current.Resources["AlbumDataTemplate"];
            if (item is Artist)
                return (DataTemplate)Application.Current.Resources["ArtistDataTemplate"];

            return base.SelectTemplateCore(item, container);
        }
    }
}
