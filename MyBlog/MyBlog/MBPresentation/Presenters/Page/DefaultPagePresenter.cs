using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.Interfaces.PresenterInterfaces.Page;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.ServiceReference;
using MBServiceLibrary;

namespace MBPresentation.Presenters.Page
{
    public class DefaultPagePresenter:IDefaultPagePresenter
    {
        private IDefaultPage _page;

        public DefaultPagePresenter(IDefaultPage page)
        {
            _page = page;
            _page.PostsReading += ReadPosts;
            _page.ImagesReading += ReadImages;
        }

        public void ReadPosts()
        {
            var svc = new ModelServiceClient();
            _page.PostList = svc.ReadPostsByCategory("all", 10).ToList();
        }

        public void ReadImages()
        {
            var svc = new ModelServiceClient();
            var result = svc.ReadImagesByCategory("all", 10);
            var rand = new Random(DateTime.Now.Millisecond);
            var imageList = new List<Image>();
            int indImage1=0, indImage2=0;
            if (result != null && result.Length >= 10)
            {
                do
                {
                    indImage1 = rand.Next(0,10);
                    indImage2 = rand.Next(0,10);
                } while (indImage1 == indImage2);

                imageList.Add(result[indImage1]);
                imageList.Add(result[indImage2]);
            }
            else
            {
                imageList = null;
            }
            _page.ImageList = imageList;
        }
    }
}
