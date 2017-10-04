using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.ServiceReference;

namespace MBPresentation.Interfaces.ViewInterfaces.Page
{
    public interface IDefaultPage
    {
        List<Post> PostList { set; }
        List<Image> ImageList { get; set; }
        Int32 Count { get; set; }
        event VoidEventHandler PostsReading;
        event VoidEventHandler ImagesReading;
    }
}
