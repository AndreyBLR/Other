using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.Interfaces.PresenterInterfaces.Page;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.ServiceReference;

namespace MBPresentation.Presenters.Page
{
    public class CommentsPagePresenter:ICommentsPagePresenter
    {
        private ICommentsPage _page;
        public CommentsPagePresenter(ICommentsPage page)
        {
            _page = page;
            _page.CommentChanging += ChangeComment;
            _page.CommentReading += ReadComment;
            _page.CommentsReading += ReadComments;
            _page.CommentRemoving += DeleteComment;
        }

        public void ReadComment()
        {
            var svc = new ModelServiceClient();
            _page.Comment = svc.ReadCommentById(_page.CommentId);
        }

        public void ReadComments()
        {
            var svc = new ModelServiceClient();
            _page.CommentList = svc.ReadCommentsByAuthorId(_page.AuthorId).OrderByDescending(item=>item.CommentDate).ToList();
        }

        public void ChangeComment()
        {
            var svc = new ModelServiceClient();
            svc.UpdateComment(_page.Comment);
        }

        public void DeleteComment()
        {
            var svc = new ModelServiceClient();
            svc.DeleteComment(_page.CommentId);
        }
    }
}
