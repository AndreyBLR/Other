using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.ServiceReference;

namespace MBPresentation.Interfaces.ViewInterfaces.Page
{
    public interface IProfilePage
    {
        event VoidEventHandler ProfileReading;
        event VoidEventHandler ProfileUpdating;
        event VoidEventHandler ImageCreating;

        Guid UserId { get; set; }
        Profile UserProfile { get; set; }
        Image Avatar { get; set; }
    }
}
