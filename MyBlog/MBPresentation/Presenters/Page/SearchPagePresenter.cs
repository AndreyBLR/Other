using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.Interfaces.PresenterInterfaces.Page;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.ServiceReference;

namespace MBPresentation.Presenters.Page
{
    public class SearchPagePresenter:ISearchPagePresenter
    {
        private ISearchPage _page;

        public SearchPagePresenter(ISearchPage page)
        {
            _page = page;
            _page.Searching += Search;
        }

        public void Search()
        {
            var svc = new ModelServiceClient();
            _page.Result = svc.SearchPosts(_page.SearchString).ToList();
        }
    }
}
