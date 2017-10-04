using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DBManagers;
using MBDatabaseModel;

namespace MBServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ModelService : IModelService
    {
        #region Read

        public Profile ReadProfileById(Guid id)
        {
            return ModelManager.ReadProfileById(id);
        }
        public Post ReadPostById(Guid id)
        {
            return ModelManager.ReadPostById(id);
        }
        public List<Post> ReadPostsByCategory(String categoryName, Int32 count)
        {
            return ModelManager.ReadPostsByCategory(categoryName, count);
        }
        public Image ReadImageById(Guid id)
        {
            return ModelManager.ReadImageById(id);
        }
        public List<Image> ReadImagesByCategory(String categoryName, Int32 count)
        {
            return ModelManager.ReadImagesByCategory(categoryName, count);
        }
        public Category ReadCategoryByName(Guid categoryId)
        {
            return ModelManager.ReadCategoryById(categoryId);
        }
        public List<Category> ReadCategories(String contentType)
        {
            return ModelManager.ReadCategories(contentType);
        }
        public List<Comment> ReadCommentsByAuthorId(Guid authorId)
        {
            return ModelManager.ReadCommentsByAuthorId(authorId);
        }
        public Comment ReadCommentById(Guid commentId)
        {
            return ModelManager.ReadCommentById(commentId);
        }
        public List<Image> ReadImagesOfPostImage(Guid postId)
        {
            return ModelManager.ReadImagesOfPostImage(postId);
        }
        public List<Warning> ReadWarnings(Guid userId)
        {
            return ModelManager.ReadWarnings(userId);
        }

        #endregion

        #region Create

        public void CreatePost(Post post)
        {
            ModelManager.CreatePost(post);
        }
        public void CreateImage(Image image)
        {
            ModelManager.CreateImage(image);
        }
        public void CreateCategory(Category category)
        {
            ModelManager.CreateCategory(category);
        }
        public void CreateProfile(Profile profile)
        {
            ModelManager.CreateProfile(profile);
        }
        public void CreateComment(Comment comment)
        {
            ModelManager.CreateComment(comment);
        }
        public void CreateImageToPost(Image image, Guid postId)
        {
            ModelManager.CreateImageToPost(image, postId);
        }
        public void CreateWarning(Warning warning)
        {
            ModelManager.CreateWarning(warning);
        }

        #endregion

        #region Delete

        public void DeletePost(Guid id)
        {
            ModelManager.DeletePost(id);
        }
        public void DeleteComment(Guid id)
        {
            ModelManager.DeleteComment(id);
        }
        public void DeleteImage(Guid id)
        {
            ModelManager.DeleteImage(id);
        }
        public void DeleteCategory(Guid categoryId)
        {
            ModelManager.DeleteCategory(categoryId);
        }
        public void DeletePostImage(Guid imageId)
        {
            ModelManager.DeletePostImageByImageId(imageId);
        }
        public void DeleteWarning(Guid warningId)
        {
            ModelManager.DeleteWarning(warningId);
        }

        #endregion

        #region Update

        public void UpdatePost(Post post)
        {
            ModelManager.UpdatePost(post);
        }
        public void UpdateProfile(Profile profile)
        {
            ModelManager.UpdateProfile(profile);
        }
        public void UpdateCategory(Category category)
        {
            ModelManager.UpdateCategory(category);
        }
        public void UpdateComment(Comment comment)
        {
            ModelManager.UpdateComment(comment);
        }
        public void UpdateImage(Image image)
        {
            ModelManager.UpdateImage(image);
        }

        #endregion

        #region Other

        public Int32 GetPostsCount(String categoryName)
        {
            return ModelManager.GetPostsCount(categoryName);
        }
        public Int32 GetImagesCount(String categoryName)
        {
            return ModelManager.GetImagesCount(categoryName);
        }
        public void BanUser(Guid userId)
        {
            ModelManager.BanUser(userId);
        }
        public void UnBanUser(Guid userId)
        {
            ModelManager.UnBanUser(userId);
        }
        public void AddWarningUser(Guid userId)
        {
            ModelManager.AddWarningUser(userId);
        }
        public void DeleteWarningUser(Guid userId)
        {
            ModelManager.DeleteWarningUser(userId);
        }
        public List<Post> SearchPosts(string searchString)
        {
            return ModelManager.SearchPosts(searchString);
        }

        #endregion
    }
}