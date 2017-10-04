using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.ServiceReference;

namespace MBPresentation.Interfaces.ViewInterfaces.Page
{
    public interface ICommentsPage
    {
        event VoidEventHandler CommentsReading;
        event VoidEventHandler CommentReading;
        event VoidEventHandler CommentChanging;
        event VoidEventHandler CommentRemoving;

        Guid CommentId { get; set; }
        Guid AuthorId { get; set; }
        List<Comment> CommentList { get; set; }
        Comment Comment { get; set; }
    }
}
