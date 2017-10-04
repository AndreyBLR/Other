using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SmartMusic.Core.DataModel.MusicCollection;

namespace SmartMusic.UILogic.DataTemplateSelectors
{
    public class FileSystemObjectTemplateSelector:DataTemplateSelector
    {
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item == null)
                return null;
            if (item is Track)
                return (DataTemplate)Application.Current.Resources["FileDataTemplate"];
            if (item is Folder)
                return (DataTemplate)Application.Current.Resources["FolderDataTemplate"];

            return base.SelectTemplateCore(item, container);
        }
    }
}
