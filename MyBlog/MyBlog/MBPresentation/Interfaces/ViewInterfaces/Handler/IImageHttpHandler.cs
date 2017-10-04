using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.ServiceReference;

namespace MBPresentation.Interfaces.ViewInterfaces.Handler
{
    public interface IImageHttpHandler
    {
        event VoidEventHandler ImageReading;
        Guid ImageId { get; set; }
        Image Image { get; set; }
    }
}
