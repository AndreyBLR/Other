using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.Interfaces.PresenterInterfaces.Page;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.ServiceReference;

namespace MBPresentation.Presenters.Page
{
    public class AdminArticlesPagePresenter:IAdminArticlesPagePresenter 
    {
        private IAdminArticlesPage _page;

        public AdminArticlesPagePresenter(IAdminArticlesPage page)
        {
            _page = page;
            _page.PostCreating += CreatePost;
            _page.PostUpdating += UpdatePost;
            _page.CategoriesReading += ReadCategories;
            _page.PostReading += ReadPost;
            _page.PostsReading += ReadPosts;
            _page.PostDeleting += DeletePost;
            _page.ImagesReading += ReadImages;
            _page.ImageCreating += CreateImage;
            _page.ImageDeleting += DeleteImage;
        }

        public void CreatePost()
        {
            var src = new ModelServiceClient();
            src.CreatePost(_page.Post);
        }
        public void UpdatePost()
        {
            var svc = new ModelServiceClient();
            svc.UpdatePost(_page.Post);
        }
        public void ReadPost()
        {
            var svc = new ModelServiceClient();
            _page.Post = svc.ReadPostById(_page.PostId);
        }
        public void ReadCategories()
        {
            var svc = new ModelServiceClient();
            _page.CategoryList = svc.ReadCategories("post").ToList();
        }
        public void ReadPosts()
        {
            var svc = new ModelServiceClient();
            _page.PostList = svc.ReadPostsByCategory(_page.CategoryName, _page.Count).ToList();
        }
        public void DeletePost()
        {
            var svc = new ModelServiceClient();
            svc.DeletePost(_page.PostId);
        }
        public void CreateImage()
        {
            var svc = new ModelServiceClient();
            svc.CreateImageToPost(_page.Image, _page.PostId);
        }
        public void ReadImages()
        {
            var svc = new ModelServiceClient();
            _page.ImageList = svc.ReadImagesOfPostImage(_page.PostId).ToList();
        }
        public void DeleteImage()
        {
            var svc = new ModelServiceClient();
            svc.DeletePostImage(_page.ImageId);
        }
    }
}
