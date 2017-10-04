using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.ServiceReference;

namespace MBPresentation.Interfaces.PresenterInterfaces.Page
{
    public interface ICategoriesPagePresenter
    {
        void ReadCategories();
        void ReadCategory();
        void CreateCategory();
        void DeleteCategory();
        void UpdateCategory();
    }
}
