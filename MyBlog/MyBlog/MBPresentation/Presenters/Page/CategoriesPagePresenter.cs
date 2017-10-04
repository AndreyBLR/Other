using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.Interfaces.PresenterInterfaces.Page;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.ServiceReference;

namespace MBPresentation.Presenters.Page
{
    public class CategoriesPagePresenter: ICategoriesPagePresenter
    {
        private ICategoriesPage _page;
        public CategoriesPagePresenter(ICategoriesPage page)
        {
            _page = page;
            _page.CategoriesReading += ReadCategories;
            _page.CategoryCreating += CreateCategory;
            _page.CategoryDeleting += DeleteCategory;
            _page.CategoryReading += ReadCategory;
            _page.CategoryUpdating += UpdateCategory;
        }

        public void ReadCategories()
        {
            var svc = new ModelServiceClient();
            _page.CategoryList = svc.ReadCategories("all").ToList();
        }

        public void ReadCategory()
        {
            var svc = new ModelServiceClient();
            _page.Category = svc.ReadCategoryByName(_page.CategoryId);
        }

        public void CreateCategory()
        {
            var svc = new ModelServiceClient();
            svc.CreateCategory(_page.Category);
        }

        public void DeleteCategory()
        {
            var svc = new ModelServiceClient();
            svc.DeleteCategory(_page.CategoryId);
        }

        public void UpdateCategory()
        {
            var svc = new ModelServiceClient();
            svc.UpdateCategory(_page.Category);
        }
    }
}
