using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.Interfaces.PresenterInterfaces.Page;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.ServiceReference;

namespace MBPresentation.Presenters.Page
{
    public class AdminGalleryPagePresenter:IAdminGalleryPagePresenter
    {
        private IAdminGalleryPage _page;

        public AdminGalleryPagePresenter(IAdminGalleryPage page)
        {
            _page = page;
            _page.ImageCreating += CreateImage;
            _page.ImageDeleting += DeleteImage;
            _page.ImagesReading += ReadImages;
            _page.CategoriesReading += ReadCategories;
            _page.ImageUpdating += UpdateImage;
            _page.ImageReading += ReadImage;
        }

        public void CreateImage()
        {
            var svc = new ModelServiceClient();
            svc.CreateImage(_page.Image);
        }
        public void DeleteImage()
        {
            var svc = new ModelServiceClient();
            svc.DeleteImage(_page.ImageId);
        }
        public void ReadImages()
        {
            var svc = new ModelServiceClient();
            _page.ImageList = svc.ReadImagesByCategory(_page.CategoryName, _page.Count).ToList();

        }
        public void ReadCategories()
        {
            var svc = new ModelServiceClient();
            _page.CategoryList = svc.ReadCategories("image").ToList();
        }
        public void UpdateImage()
        {
            var svc = new ModelServiceClient();
            svc.UpdateImage(_page.Image);
        }

        public void ReadImage()
        {
            var svc = new ModelServiceClient();
            _page.Image = svc.ReadImageById(_page.ImageId);
        }
    }
}
