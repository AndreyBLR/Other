using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBPresentation.Interfaces.PresenterInterfaces.Page
{
    public interface IAdminGalleryPagePresenter
    {
        void CreateImage();
        void DeleteImage();
        void ReadImages();
        void ReadCategories();
        void UpdateImage();
        void ReadImage();
    }
}
