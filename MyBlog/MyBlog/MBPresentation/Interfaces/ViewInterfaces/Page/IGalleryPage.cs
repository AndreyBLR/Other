using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.ServiceReference;

namespace MBPresentation.Interfaces.ViewInterfaces.Page
{
    public interface IGalleryPage
    {
        event VoidEventHandler ImagesReading;
        event VoidEventHandler TagsReading;

        List<Tag> TagList { get; set; }
        List<Image> ImageList { get; set; }
        Int32 Count { get; set; }
        String CategoryName { get; set; }
    }
}
