using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MBPresentation;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.Presenters.Page;
using MBPresentation.ServiceReference;
using MyBlog.Controls;

namespace MyBlog
{
    public partial class journal : System.Web.UI.Page, IJournalPage
    {
        private readonly JournalPagePresenter _presenter;
        
        public List<Post> PostList { get; set; }
        public List<Tag> TagList { get; set; }
        public String CategoryName { get; set; }
        public int Count { get; set; }

        public event VoidEventHandler PostsReading;
        public event VoidEventHandler TagsReading;

        public journal()
        {
            _presenter = new JournalPagePresenter(this);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (this.IsSet("category"))
            {
                ReadPosts(Request.Params["category"], 15);
                PostList = PostList.ChangeExcerpt();
                lstArticles.DataSource = PostList;
                lstArticles.DataBind();
                ReadTags();
                tagsCloud.TagList = TagList;
                tagsCloud.PageLink = "../journal.aspx";
            }
            else
            {
                Response.Redirect("~/journal.aspx?category=all");
            }
        }

        public void ReadTags()
        {
            if(TagsReading != null)
            {
                TagsReading();
            }
        }
        public void ReadPosts(String categoryName, Int32 count)
        {
            Count = count;
            CategoryName = categoryName;
            if(PostsReading != null)
            {
                PostsReading();
            }
        }
       
    }
}