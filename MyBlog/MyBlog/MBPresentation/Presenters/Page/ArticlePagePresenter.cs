using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.Interfaces.PresenterInterfaces.Page;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.ServiceReference;

namespace MBPresentation.Presenters.Page
{
    public class ArticlePagePresenter : IArticlePagePresenter
    {
        private readonly IArticlePage _page;

        public ArticlePagePresenter(IArticlePage page)
        {
            _page = page;
            _page.PostReading += ReadPost;
            _page.CommentCreating += CreateComment;
            _page.UserStatusReading += ReadUserStatus;
        }

        public void ReadPost()
        {
            try
            {
                var svc = new ModelServiceClient();
                _page.Post = svc.ReadPostById(_page.PostId);
                _page.Post.Comments = _page.Post.Comments.OrderByDescending(item => item.CommentDate).ToArray();
            }
            catch (Exception ex)
            {
               
            }
            
        }
        public void CreateComment()
        {
            var svc = new ModelServiceClient();
            svc.CreateComment(_page.Comment);
        }
        public void ReadUserStatus()
        {
            var svc = new ModelServiceClient();
            _page.IsBanned = Convert.ToBoolean(svc.ReadProfileById(_page.UserId).IsBanned);
        }
    }
}
