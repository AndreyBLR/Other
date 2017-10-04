using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation;
using MBPresentation.Interfaces.PresenterInterfaces.Page;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.ServiceReference;

namespace MBPresentation.Presenters.Page
{
    public class GalleryPagePresenter: IGalleryPagePresenter
    {
        private IGalleryPage _page;
        public GalleryPagePresenter(IGalleryPage page)
        {
            _page = page;
            _page.ImagesReading += ReadImages;
            _page.TagsReading += ReadTags;
        }


        public void ReadImages()
        {
            var svc = new ModelServiceClient();
            _page.ImageList = svc.ReadImagesByCategory(_page.CategoryName, _page.Count).ToList();
        }

        public void ReadTags()
        {
            var svc = new ModelServiceClient();
            var categoryList = svc.ReadCategories("image");
            var tagList = new List<Tag>();
            foreach (var category in categoryList)
            {
                tagList.Add(new Tag()
                                {
                                    Count = svc.GetImagesCount(category.CategoryName), 
                                    TagName = category.CategoryName
                                });
            }
            _page.TagList = tagList;
        }
    }
}
