using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.ServiceReference;

namespace MBPresentation.Interfaces.ViewInterfaces.Page
{
    public interface IWarningsPage
    {
        event VoidEventHandler WarningsReading;
        event VoidEventHandler WarningAdding;
        event VoidEventHandler WarningDeleting;
        event VoidEventHandler UserStatusReading;

        List<Warning> WarningList { get; set; }
        Guid UserId { get; set; }
        Guid WarningId { get; set; }
        Warning Warning { get; set; }
        Boolean IsBanned { get; set; }
    }
}
