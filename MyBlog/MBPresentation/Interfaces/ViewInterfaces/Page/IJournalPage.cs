using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.ServiceReference;

namespace MBPresentation.Interfaces.ViewInterfaces.Page
{
    public interface IJournalPage
    {
        event VoidEventHandler PostsReading;
        event VoidEventHandler TagsReading;

        List<Post> PostList { get; set; }
        List<Tag> TagList { get; set; }
        String CategoryName { get; set; }
        Int32 Count { get; set; }
    }
}
