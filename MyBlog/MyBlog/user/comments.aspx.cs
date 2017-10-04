using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI.WebControls;
using Classes;
using MBPresentation;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.Presenters.Page;
using MBPresentation.ServiceReference;

namespace MyBlog.user
{
    public partial class comments : System.Web.UI.Page, ICommentsPage
    {
        private CommentsPagePresenter _presenter;

        public event VoidEventHandler CommentsReading;
        public event VoidEventHandler CommentReading;
        public event VoidEventHandler CommentChanging;
        public event VoidEventHandler CommentRemoving;

        public Guid CommentId { get; set; }
        public Guid AuthorId { get; set; }
        public List<Comment> CommentList { get; set; }
        public Comment Comment { get; set; }

        public comments()
        {
            _presenter = new CommentsPagePresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void ChangeBtnClick(object sender, EventArgs e)
        {
            try
            {
                ReadComment(Guid.Parse(Request.Params["commentID"]));
                Comment.CommentText = text.Text;
                ChangeComment(Comment);
                Response.Redirect("~/article.aspx?id=" + Comment.CommentPostId);
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error(User.Identity.IsAuthenticated ? User.Identity.Name : "anonimus" + "  " + ex.Message);
            }
            
        }
        protected void LstCommentsItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                var deleteCommentPh = (PlaceHolder)e.Item.FindControl("deleteCommentPh");
                var changeCommentPh = (PlaceHolder)e.Item.FindControl("changeCommentPh");
                if (e.Item.DataItem != null)
                {
                    var dateItem = (Comment)e.Item.DataItem;
                    if (dateItem.aspnet_Users.UserName == User.Identity.Name || Roles.IsUserInRole("Admin"))
                    {
                        deleteCommentPh.Controls.Add(new HyperLink
                        {
                            Text = "Delete",
                            NavigateUrl = "~/user/comments.aspx?userId=" + dateItem.aspnet_Users.UserId + "&commentId=" + dateItem.CommentId + "&action=delete"
                        });
                        changeCommentPh.Controls.Add(new HyperLink
                        {
                            Text = "Change",
                            NavigateUrl = "~/user/comments.aspx?userId=" + dateItem.aspnet_Users.UserId + "&commentId=" + dateItem.CommentId + "&action=change"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error(User.Identity.IsAuthenticated ? User.Identity.Name : "anonimus" + "  " + ex.Message);
            }

            
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                Guid userId;
                if (this.IsSet("userId") && Guid.TryParse(Request.Params["userId"], out userId))
                {
                    Guid commentId;
                    if (this.IsSet("action") && this.IsSet("commentId") && Guid.TryParse(Request.Params["commentId"], out commentId))
                    {
                        switch (Request.Params["action"])
                        {
                            case "change":
                                multiView.SetActiveView(changeCommentView);
                                ReadComment(commentId);
                                text.Text = Comment.CommentText;
                                break;
                            case "delete":
                                if (userId == (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey)
                                {
                                    DeleteComment(commentId);
                                }
                                Response.Redirect(Request.UrlReferrer.AbsoluteUri != null ? Request.UrlReferrer.AbsoluteUri : "~/comments.aspx?userId=" + userId);
                                break;
                            default:
                                Response.Redirect("~/user/comments.aspx?userId=" + userId);
                                break;
                        }
                    }
                    else
                    {
                        multiView.SetActiveView(commentsView);
                        ReadCommentList(userId);
                        lstComments.DataSource = CommentList;
                        lstComments.DataBind();
                    }
                }
                else
                {
                    multiView.SetActiveView(noCommentsView);
                }
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error(User.Identity.IsAuthenticated ? User.Identity.Name : "anonimus" + "  " + ex.Message);
            }

            
        }

        public void ReadCommentList(Guid userId)
        {
            AuthorId = userId;
            if(CommentsReading != null)
            {
                CommentsReading();
            }
        }
        public void ReadComment(Guid commentId)
        {
            CommentId = commentId;
            if(CommentReading != null)
            {
                CommentReading();
            }
        }
        public void DeleteComment(Guid commentId)
        {
            CommentId = commentId;
            if(CommentRemoving != null)
            {
                CommentRemoving();
            }
        }
        public void ChangeComment(Comment comment)
        {
            Comment = comment;
            if (CommentChanging != null)
            {
                CommentChanging();
            }
        }
    }
}