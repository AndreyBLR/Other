using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.ServiceReference;

namespace MBPresentation.Interfaces.ViewInterfaces.Page
{
    public interface IUsersPage
    {
        event VoidEventHandler UserBanning;
        event VoidEventHandler UserUnbannig;
        event VoidEventHandler WarningAdding;
        event VoidEventHandler WarningDeleting;
        event VoidEventHandler UserReading;
        event VoidEventHandler UserStatusReading;

        Guid UserId { get; set; }
        Profile UserProfile { get; set; }
        Boolean IsBanned { get; set; }
    }
}
