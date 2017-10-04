using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.ServiceReference;

namespace MBPresentation.Interfaces.PresenterInterfaces.Page
{
    public interface IAdminArticlesPagePresenter
    {
        void ReadPosts();
        void ReadPost();
        void CreatePost();
        void UpdatePost();
        void DeletePost();
        void ReadCategories();
        void CreateImage();
        void ReadImages();
        void DeleteImage();
    }
}
