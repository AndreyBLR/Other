using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using MBPresentation;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.Presenters.Page;
using MBPresentation.ServiceReference;
using Image = MBPresentation.ServiceReference.Image;

namespace MyBlog
{
    public partial class _default : System.Web.UI.Page, IDefaultPage
    {
        
        private DefaultPagePresenter _presenter;

        public List<Post> PostList { get; set; }
        public List<Image> ImageList { get; set; }
        public Int32 Count { get; set; }

        public event VoidEventHandler PostsReading;
        public event VoidEventHandler ImagesReading;

        public _default()
        {
            _presenter = new DefaultPagePresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "MyBlog Home";
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                var lblUserName = (Label) loginView.FindControl("lblUserName");
                lblUserName.Text = User.Identity.Name;
            }
            ReadImages();
            if (ImageList != null)
            {
                image1.ImageUrl = @"/Handlers/image.ashx?id=" + ImageList[0].ImageId + "&mode=small";
                image2.ImageUrl = @"/Handlers/image.ashx?id=" + ImageList[1].ImageId + "&mode=small";
            }
            else
            {
                image1.AlternateText = "No image";
                image2.AlternateText = "No image";
            }

            ReadPosts();
            if(PostList != null)
            {
                PostList = PostList.ChangeExcerpt();
            }
            lstPosts.DataSource = PostList;
            lstPosts.DataBind();
        }

        public void ReadPosts()
        {
            if(PostsReading != null)
            {
                PostsReading();
            }
        }
        public void ReadImages()
        {
            if(ImagesReading != null)
            {
                ImagesReading();
            }
        }

        
    }
}