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

namespace MyBlog
{
    public partial class search : System.Web.UI.Page, ISearchPage
    {
        private SearchPagePresenter _presenter;

        public event VoidEventHandler Searching;

        public string SearchString { get; set; }
        public List<Post> Result { get; set; }

        public search()
        {
            _presenter = new SearchPagePresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if(this.IsSet("searchStr"))
            {
                Search(Request.Params["searchStr"]);
                lstSearchResult.DataSource = Result;
                lstSearchResult.DataBind();
            }
        }

        public void Search(string searchString)
        {
            SearchString = searchString;
            if(Searching != null)
            {
                Searching();
            }
        }

        protected void LstSearchResultItemCreated(object sender, ListViewItemEventArgs e)
        {
            var item = (ListViewItem) e.Item;
            var post = (Post) item.DataItem;
            var lastIndex = 0;

            int index = post.PostText.IndexOf(SearchString);
                lastIndex = post.PostText.LastIndexOf(SearchString);

                if ((index - 100) > 0 && (lastIndex + 100) < post.PostText.Length)// обрезать строку с двух сторон
                {
                    post.PostExcerpt = "..." + post.PostText.Remove(0, index - 100);
                    post.PostExcerpt = post.PostExcerpt.Remove(post.PostExcerpt.LastIndexOf(SearchString) + 100) + "...";
                }
                else if ((index - 100) > 0 && (lastIndex + 100) > post.PostText.Length)// обрезать строку спереди
                {
                    post.PostExcerpt = "..." + post.PostText.Remove(0, index - 100);
                }
                else if ((index - 100) < 0 && (lastIndex + 100) < post.PostText.Length)// обрезать строку сзади
                {
                    post.PostExcerpt = post.PostText.Remove(lastIndex + 100) + "...";
                }
                else //не обрезать строку
                {
                    post.PostExcerpt = post.PostText;
                }
            e.Item.DataItem = post;
        }
    }
}