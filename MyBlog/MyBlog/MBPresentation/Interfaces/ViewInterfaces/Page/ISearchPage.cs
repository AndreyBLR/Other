using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.ServiceReference;

namespace MBPresentation.Interfaces.ViewInterfaces.Page
{
    public interface ISearchPage
    {
        event VoidEventHandler Searching;

        String SearchString { get; set; }
        List<Post> Result { get; set; }
    }
}
