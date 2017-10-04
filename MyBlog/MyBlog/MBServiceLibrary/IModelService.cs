using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MBDatabaseModel;

namespace MBServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IModelService
    {
        #region Read

        [OperationContract]
        Profile ReadProfileById(Guid id);
        [OperationContract]
        Post ReadPostById(Guid id);
        [OperationContract]
        List<Post> ReadPostsByCategory(String categoryName, Int32 count);
        [OperationContract]
        Image ReadImageById(Guid id);
        [OperationContract]
        List<Image> ReadImagesByCategory(String categoryName, Int32 count);
        [OperationContract]
        Category ReadCategoryByName(Guid categoryId);
        [OperationContract]
        List<Category> ReadCategories(String contentType);
        [OperationContract]
        List<Comment> ReadCommentsByAuthorId(Guid authorId);
        [OperationContract]
        Comment ReadCommentById(Guid commentId);
        [OperationContract]
        List<Image> ReadImagesOfPostImage(Guid postId);
        [OperationContract]
        List<Warning> ReadWarnings(Guid userId);

        #endregion

        #region Create

        [OperationContract]
        void CreatePost(Post post);
        [OperationContract]
        void CreateImage(Image image);
        [OperationContract]
        void CreateCategory(Category category);
        [OperationContract]
        void CreateProfile(Profile profile);
        [OperationContract]
        void CreateComment(Comment comment);
        [OperationContract]
        void CreateImageToPost(Image image, Guid postId);
        [OperationContract]
        void CreateWarning(Warning warning);

        #endregion

        #region Delete

        [OperationContract]
        void DeletePost(Guid id);
        [OperationContract]
        void DeleteComment(Guid id);
        [OperationContract]
        void DeleteImage(Guid id);
        [OperationContract]
        void DeleteCategory(Guid categoryId);
        [OperationContract]
        void DeletePostImage(Guid imageId);
        [OperationContract]
        void DeleteWarning(Guid warningId);
        #endregion

        #region Update

        [OperationContract]
        void UpdatePost(Post post);
        [OperationContract]
        void UpdateProfile(Profile profile);
        [OperationContract]
        void UpdateCategory(Category category);
        [OperationContract]
        void UpdateComment(Comment comment);
        [OperationContract]
        void UpdateImage(Image image);

        #endregion

        #region Other

        [OperationContract]
        Int32 GetPostsCount(String categoryName);
        [OperationContract]
        Int32 GetImagesCount(String categoryName);
        [OperationContract]
        void BanUser(Guid userId);
        [OperationContract]
        void UnBanUser(Guid userId);
        [OperationContract]
        void AddWarningUser(Guid userId);
        [OperationContract]
        void DeleteWarningUser(Guid userId);
        [OperationContract]
        List<Post> SearchPosts(String searchString);

        #endregion
    }
}
