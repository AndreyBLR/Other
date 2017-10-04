using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.ServiceReference;

namespace MBPresentation.Interfaces.ViewInterfaces.Page
{
    public interface IAdminArticlesPage
    {

        event VoidEventHandler PostsReading;
        event VoidEventHandler PostReading;
        event VoidEventHandler PostCreating;
        event VoidEventHandler PostUpdating;
        event VoidEventHandler PostDeleting;
        event VoidEventHandler CategoriesReading;
        event VoidEventHandler ImageCreating;
        event VoidEventHandler ImagesReading;
        event VoidEventHandler ImageDeleting;

        Guid PostId { get; set; }
        List<Post> PostList { get; set; }
        String CategoryName { get; set; }
        Int32 Count { get; set; }
        Post Post { get; set; }
        List<Category> CategoryList { get; set; }
        Guid ImageId { get; set; }
        Image Image { get; set; }
        List<Image> ImageList { get; set; }
    }
}
