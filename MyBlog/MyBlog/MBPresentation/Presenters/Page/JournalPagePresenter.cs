using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.Interfaces.PresenterInterfaces.Page;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.ServiceReference;

namespace MBPresentation.Presenters.Page
{
    public class JournalPagePresenter:IJournalPagePresenter
    {
        private readonly IJournalPage _page;

        public JournalPagePresenter(IJournalPage page)
        {
            _page = page;
            _page.PostsReading += ReadPosts;
            _page.TagsReading += ReadTags;
        }

        public void ReadPosts()
        {
            var svc = new ModelServiceClient();
            _page.PostList = svc.ReadPostsByCategory(_page.CategoryName, _page.Count).ToList();
        }

        public void ReadTags()
        {
            var svc = new ModelServiceClient();
            var categoryList = svc.ReadCategories("post");
            var tagList = new List<Tag>();
            foreach (var category in categoryList)
            {
                tagList.Add(new Tag()
                {
                    Count = svc.GetPostsCount(category.CategoryName),
                    TagName = category.CategoryName
                });
            }
            _page.TagList = tagList;
        }
    }
}
