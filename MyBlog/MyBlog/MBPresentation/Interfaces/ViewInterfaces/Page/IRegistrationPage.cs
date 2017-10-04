using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.ServiceReference;

namespace MBPresentation.Interfaces.ViewInterfaces.Page
{
    public interface IRegistrationPage
    {
        event VoidEventHandler ProfileCreating;

        Profile UserProfile { get; set; }
    }
}
