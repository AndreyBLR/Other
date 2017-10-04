using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using Classes;
using MBPresentation;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.Presenters.Page;
using MBPresentation.ServiceReference;
using MSCaptcha;

namespace MyBlog
{
    public partial class article : System.Web.UI.Page, IArticlePage
    {
        private ArticlePagePresenter _presenter;

        public event VoidEventHandler UserStatusReading;

        public Guid UserId { get; set; }

        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public Comment Comment { get; set; }
        public bool IsBanned { get; set; }
        public bool IsAdmin { get; set; }


        public event VoidEventHandler PostReading;
        public event VoidEventHandler CommentCreating;

        public article()
        {
            _presenter = new ArticlePagePresenter(this);
            IsAdmin = Roles.IsUserInRole("Admin");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void AddBtnClick(object sender, EventArgs e)
        {
            try
            {
                var captcha = (CaptchaControl)viewForm.FindControl("captcha");
                var txbCaptcha = (TextBox)viewForm.FindControl("txbCaptcha");
                var textBox = (TextBox)viewForm.FindControl("txbComment");
                captcha.ValidateCaptcha(txbCaptcha.Text.Trim());
                if(captcha.UserValidated)
                {
                    
                    Guid postId;
                    if (this.IsSet("id") && Guid.TryParse(Request.Params["id"], out postId))
                    {
                        var newComment = new Comment
                                             {
                                                 CommentId = Guid.NewGuid(),
                                                 CommentAuthorId =
                                                     (Guid) Membership.GetUser(User.Identity.Name).ProviderUserKey,
                                                 CommentDate = DateTime.Now,
                                                 CommentText = textBox.Text,
                                                 CommentPostId = postId
                                             };
                        CreateComment(newComment);
                    }
                }
                else
                {
                    Session["TextComment"] = textBox.Text;
                }
            }
            catch(Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error(User.Identity.Name + "  " + ex.Message);
                Response.Redirect("~/journal.aspx?category=all");
            }
            
            
        }
        public void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (this.IsSet("id"))
                {
                    Guid postId;
                    if (this.IsSet("id") && Guid.TryParse(Request.Params["id"], out postId))
                    {
                        ReadPost(postId);
                        if (Post != null)
                        {
                            Title = Post.PostTitle;
                            titleArticle.Text = Post.PostTitle;
                            metaArticle.Text = Post.PostDate + "           " + Post.aspnet_Users.UserName;
                            textArticle.Text = Helper.ParseText(Post.PostText);
                            lstComments.DataSource = Post.Comments;
                            lstComments.DataBind();
                            if (Post.CommentStatus == false)
                            {
                                viewForm.Visible = false;
                            }
                            if (!IsPostBack && Session["TextComment"] != null)
                            {
                                var textBox = (TextBox)viewForm.FindControl("txbComment");
                                textBox.Text = Session["TextComment"].ToString();
                            }
                        }
                        else
                        {
                            Title = "Article not found";
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/default.aspx");
                }
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error(User.Identity.IsAuthenticated ? User.Identity.Name : "anonimus" + "  " + ex.Message);
            }
            
        }

        public void CreateComment(Comment comment)
        {
            Comment = comment;
            if (CommentCreating != null)
            {
                CommentCreating();
            }
        }
        public void ReadPost(Guid postId)
        {
            PostId = postId;
            if(PostReading != null)
            {
                PostReading();
            }
        }
        public void ReadUserStatus(Guid userId)
        {
            UserId = userId;
            if(UserStatusReading != null)
            {
                UserStatusReading();
            }
        }

        protected void CommentsItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                var deleteCommentPh = (PlaceHolder)e.Item.FindControl("deleteCommentPh");
                var changeCommentPh = (PlaceHolder)e.Item.FindControl("changeCommentPh");
                var allCommentsPh = (PlaceHolder)e.Item.FindControl("allCommentsPh");
                if (e.Item.DataItem != null)
                {
                    var dateItem = (Comment)e.Item.DataItem;

                    if (IsAdmin)
                    {
                        var addWarningPh = (PlaceHolder)e.Item.FindControl("addWarningPh");
                        var delWarningPh = (PlaceHolder)e.Item.FindControl("delWarningPh");
                        var banUserPh = (PlaceHolder)e.Item.FindControl("banUserPh");
                        addWarningPh.Controls.Add(new HyperLink
                                                      {
                                                          Text = "Warnings",
                                                          NavigateUrl = "../user/warnings.aspx?userId=" + dateItem.CommentAuthorId
                        });
                        delWarningPh.Controls.Add(new HyperLink
                        {
                            Text = "+",
                            NavigateUrl = "../user/warnings.aspx?userId=" + dateItem.CommentAuthorId + "&action=add"
                        });
                        if (Convert.ToBoolean(dateItem.aspnet_Users.Profiles.IsBanned))
                        {
                            banUserPh.Controls.Add(new HyperLink
                            {
                                Text = "Unban",
                                NavigateUrl = "../admin/users.aspx?id=" + dateItem.CommentAuthorId + "&action=unban"
                            });
                        }
                        else
                        {
                            banUserPh.Controls.Add(new HyperLink
                            {
                                Text = "Ban",
                                NavigateUrl = "../admin/users.aspx?id=" + dateItem.CommentAuthorId + "&action=ban"
                            });
                        }
                    }
                    if (dateItem.aspnet_Users.UserName == User.Identity.Name || IsAdmin)
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
                    if (User.Identity.IsAuthenticated)
                    {
                        allCommentsPh.Controls.Add(new HyperLink
                        {
                            Text = "All Comments",
                            NavigateUrl = "~/user/comments.aspx?userId=" + dateItem.aspnet_Users.UserId
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error(User.Identity.IsAuthenticated ? User.Identity.Name : "anonimus" + "  " + ex.Message);
                Response.Redirect("~/articles.aspx?category=all");
            }
            
            
        }
    }
}