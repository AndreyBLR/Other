using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Classes;
using MBPresentation;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.Presenters.Page;
using MBPresentation.ServiceReference;
using Image = MBPresentation.ServiceReference.Image;

namespace MyBlog.Images
{
    public partial class articles : System.Web.UI.Page, IAdminArticlesPage
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public List<Post> PostList { get; set; }
        public string CategoryName { get; set; }
        public List<Category> CategoryList { get; set; }
        public Guid ImageId { get; set; }
        public Image Image { get; set; }
        public List<Image> ImageList { get; set; }
        public int Count { get; set; }

        public event VoidEventHandler PostCreating;
        public event VoidEventHandler PostUpdating;
        public event VoidEventHandler PostReading;
        public event VoidEventHandler CategoriesReading;
        public event VoidEventHandler PostsReading;
        public event VoidEventHandler PostDeleting;
        public event VoidEventHandler ImageCreating;
        public event VoidEventHandler ImagesReading;
        public event VoidEventHandler ImageDeleting;

        public articles()
        {
            var presenter = new AdminArticlesPagePresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                ReadCategories();
                Cache.Insert("CategoryList", CategoryList, null, DateTime.Now.AddMinutes(5d), System.Web.Caching.Cache.NoSlidingExpiration);
                return;
            }
            if (Request.UrlReferrer.LocalPath != "/admin/articles.aspx" || Cache["CategoryList"] == null)
            {
                ReadCategories();
                Cache.Insert("CategoryList", CategoryList, null, DateTime.Now.AddMinutes(5d), System.Web.Caching.Cache.NoSlidingExpiration);
                return;
            }
        }

        protected void AddBtnClick(object sender, EventArgs e)
        {
            try
            {
                var post = new Post
                {
                    PostId = Guid.NewGuid(),
                    PostTitle = title.Text,
                    PostText = text.Text,
                    PostAuthorId = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey,
                    PostDate = DateTime.Now,
                    PostExcerpt = excerpt.Text,
                    CommentCount = 0,
                    CommentStatus = comment.Checked,
                    PostRating = 0,
                    PostType = "Article",
                    PostParentId = null,
                    PostDateModified = null,
                    PostCategoryId = (from category in (List<Category>)Cache["CategoryList"]
                                      where category.CategoryName == ddlCategory.SelectedItem.Text
                                      select category.CategoryId).First()
                };
                post.PostLink = "../article.aspx?id=" + post.PostId;
                CreatePost(post);
                Response.Redirect(post.PostLink);
            }
            catch(Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error(User.Identity.Name + "  " + ex.Message);
                Response.Redirect("~/admin/articles.aspx?category=all");
            }
            
        }
        protected void SaveBtnClick(object sender, EventArgs e)
        {
            try
            {
                var post = (Post)Cache["Post"];
                post.PostTitle = title.Text;
                post.PostText = text.Text;
                post.PostExcerpt = excerpt.Text;
                post.PostDateModified = DateTime.Now;
                post.CommentStatus = comment.Checked;
                if (Cache["CategoryList"] == null)
                {
                    ReadCategories();
                    Cache.Insert("CategoryList", CategoryList, null, DateTime.Now.AddMinutes(5d), System.Web.Caching.Cache.NoSlidingExpiration);
                }
                post.PostCategoryId = (from category in (List<Category>)Cache["CategoryList"]
                                       where category.CategoryName == ddlCategory.SelectedItem.Text
                                       select category.CategoryId).First();
                UpdatePost(post);
                Cache.Remove("Post");
                Response.Redirect("articles.aspx?category=all");
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error(User.Identity.Name + "  " + ex.Message);
                Response.Redirect("~/admin/articles.aspx?category=all");
            }
        }
        protected void LoadBtnClick(object sender, EventArgs e)
        {
            try
            {
                var buf = new byte[fileUpload.PostedFile.InputStream.Length];
                fileUpload.PostedFile.InputStream.Read(buf, 0, Convert.ToInt32(fileUpload.PostedFile.InputStream.Length));
                var image = new Image
                {
                    ImageId = Guid.NewGuid(),
                    ImageSmall = Helper.ScaleImage(buf, 200, 150),
                    ImageFile = Helper.ScaleImage(buf, 640, 478),
                    ImageDate = DateTime.Now,
                    ImageCategoryId = Guid.Parse("f9435c4b-9dfb-47e6-ae27-45c2bab499e3")
                };
                CreateImage(image, Guid.Parse(Request.Params["postId"]));
            }
            catch(Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error(User.Identity.Name + "  " + ex.Message);
                Response.Redirect("~/admin/articles.aspx?category=all");
            }
            
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                ddlCategory.DataSource = from item in (List<Category>)Cache["CategoryList"] select item.CategoryName;
                ddlCategory.DataBind();
                if (this.IsSet("action"))
                {
                    Guid id;
                    switch (Request.Params["action"])
                    {
                        case "add":
                            multiView.SetActiveView(forms);
                            plLoadImageForm.Visible = false;
                            SaveBtn.Visible = false;
                            break;
                        case "change":
                            if (this.IsSet("postId") && Guid.TryParse(Request.Params["postId"], out id))
                            {
                                multiView.SetActiveView(forms);
                                AddBtn.Visible = false;
                                ReadPost(id);
                                Cache.Insert("Post", Post);
                                title.Text = Post.PostTitle;
                                text.Text = Post.PostText;
                                excerpt.Text = Post.PostExcerpt;
                                ddlCategory.SelectedItem.Text = Post.Categories.CategoryName;
                                comment.Checked = Convert.ToBoolean(Post.CommentStatus);
                            }
                            else
                            {
                                Response.Redirect("~/admin/articles.aspx?categories=all");
                            }
                            break;
                        case "delete":
                            if (this.IsSet("postId") && Guid.TryParse(Request.Params["postId"], out id))
                            {
                                DeletePost(id);
                                Response.Redirect(Request.UrlReferrer.AbsoluteUri);
                            }
                            Response.Redirect("~/admin/articles.aspx?category=all");
                            break;
                        case "delete_image":
                            if (this.IsSet("imageId") && Guid.TryParse(Request.Params["imageId"], out id))
                            {
                                DeleteImage(id);
                            }
                            Response.Redirect(Request.UrlReferrer.AbsoluteUri);
                            break;
                        default:
                            Response.Redirect("~/admin/articles.aspx?category=all");
                            break;
                    }
                }
                else if (this.IsSet("category"))
                {
                    multiView.SetActiveView(articlesView);
                    ReadPosts(Request.Params["category"], 40);
                    lstArticles.DataSource = PostList;
                    lstArticles.DataBind();
                }
                else
                {
                    Response.Redirect("articles.aspx?category=all");
                }
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error(User.Identity.Name + "  " + ex.Message);
                Response.Redirect("~/admin/articles.aspx?category=all");
            }
        }

        public string MakeHtmlTable()
        {
            try
            {
                if (ImageList != null)
                {
                    var html = new StringBuilder();
                    int i = 0;
                    while (ImageList.Count > i)
                    {
                        html.Append("<table width='100%'>");
                        html.Append("<tr align='center'>");
                        int j;
                        for (j = i; j < i + 2; j++)
                        {
                            if (ImageList.Count <= j)
                            {
                                break;
                            }
                            html.Append("<td>");
                            html.Append("<table cellpadding='10px'>");
                            html.Append("<tr>");
                            html.Append("<td>" +
                                        "<img src='../Handlers/image.ashx?id=" + ImageList[j].ImageId +
                                        "&mode=small' /> </td>");
                            html.Append("</tr>");
                            html.Append("<tr>");
                            html.Append("<td>" +
                                        ImageList[j].ImageId +
                                        "</td>");
                            html.Append("</tr>");
                            html.Append("<tr>");
                            html.Append("<td>" +
                                        ImageList[j].ImageDescription +
                                        "</td>");
                            html.Append("</tr>");
                            html.Append("<tr>");
                            html.Append("<td>" +
                                        "<a href='../admin/articles.aspx?imageId=" + ImageList[j].ImageId +
                                        "&action=delete_image'>Delete</a>" +
                                        "</td>");
                            html.Append("</tr>");
                            html.Append("</table>");
                        }
                        i = j;
                        html.Append("</tr>");
                        html.Append("</table>");
                    }
                    return html.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error(User.Identity.Name + "  " + ex.Message);
                Response.Redirect("~/admin/articles.aspx?category=all");
            }
            return null;
        }

        public void CreatePost(Post post)
        {
            Post = post;
            if(PostCreating != null)
            {
                PostCreating();
            }
        }
        public void DeletePost(Guid postId)
        {
            PostId = postId;
            if (PostDeleting != null)
            {
                PostDeleting();
            }
        }
        public void UpdatePost(Post post)
        {
            Post = post;
            if (PostUpdating != null)
            {
                PostUpdating();
            }
        }
        public void ReadPost(Guid postId)
        {
            PostId = postId;
            if (PostReading != null)
            {
                PostReading();
            }
        }
        public void ReadPosts(String categoryName, Int32 count)
        {
            CategoryName = categoryName;
            Count = count;
            if (PostsReading != null)
            {
                PostsReading();
            }
        }
        public void ReadCategories()
        {
            if (CategoriesReading != null)
            {
                CategoriesReading();
            }
        }
        public void CreateImage(Image image, Guid postId)
        {
            Image = image;
            PostId = postId;
            if (ImageCreating != null)
            {
                ImageCreating();
            }
        }
        public void ReadImages(Guid postId)
        {
            PostId = postId;
            if (ImagesReading != null)
            {
                ImagesReading();
            }
        }
        public void DeleteImage(Guid imageId)
        {
            ImageId = imageId;
            if (ImageDeleting != null)
            {
                ImageDeleting();
            }
        }
    }
}