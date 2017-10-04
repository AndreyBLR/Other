using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.ServiceReference;

namespace MBPresentation.Interfaces.ViewInterfaces.Page
{
    public interface IAdminGalleryPage
    {
        List<Image> ImageList { get; set; }
        Image Image { get; set; }
        Guid ImageId { get; set; }
        List<Category> CategoryList { get; set; }
        String CategoryName { get; set; }
        Int32 Count { get; set; }

        event VoidEventHandler ImageReading;
        event VoidEventHandler ImageCreating;
        event VoidEventHandler ImageDeleting;
        event VoidEventHandler ImagesReading;
        event VoidEventHandler ImageUpdating;
        event VoidEventHandler CategoriesReading;
    }
}
