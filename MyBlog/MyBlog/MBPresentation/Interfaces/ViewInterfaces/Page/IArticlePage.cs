using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.ServiceReference;

namespace MBPresentation.Interfaces.ViewInterfaces.Page
{
    public interface IArticlePage
    {
        event VoidEventHandler PostReading;
        event VoidEventHandler CommentCreating;
        event VoidEventHandler UserStatusReading;

        Guid UserId { get; set; }
        Guid PostId { get; set; }
        Post Post { get; set; }
        Comment Comment { get; set; }
        Boolean IsBanned { get; set; }
    }
}
