using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using MBDatabaseModel;

namespace DBManagers
{
    public static class ModelManager
    {
        #region READ

        public static List<aspnet_Users> ReadUsers()
        {
            try
            {
                var dataContext = new DataContext();
                var query = from item in dataContext.aspnet_Users.Include("aspnet_Roles").Include("Profiles") 
                            select item;
                return query.ToList();
            }
            catch(Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("ReadUsers():   " + ex.Message);
                return null;
            }
            
        }
        public static Profile ReadProfileById(Guid id)
        {
            try
            {
                var dataCoxtext = new DataContext();
                var query = (from profile in dataCoxtext.Profiles
                            where profile.UserId == id
                            select profile).First();
                return query;
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("ReadProfileById(" + id + "):   " + ex.Message);
                return null;
            }
            
        }
        public static Post ReadPostById(Guid postId)
        {
            try
            {
                var dataCoxtext = new DataContext();
                var query = (from post in dataCoxtext.Posts.Include("Comments")
                             where post.PostId == postId
                             select post).First();
                
                foreach (var comment in query.Comments)
                {
                    if (!comment.aspnet_UsersReference.IsLoaded)
                    {
                        comment.aspnet_UsersReference.Load();
                        comment.aspnet_Users.ProfilesReference.Load();
                    }
                }
                return query;
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("ReadPostById(" + postId + "):   " + ex.Message);
                return null;
            }
            
        }
        public static List<Post> ReadPostsByCategory(String categoryName, Int32 count)
        {
            try
            {
                var dataCoxtext = new DataContext();
                if (categoryName == "all")
                {
                    var query = (from post in dataCoxtext.Posts
                                 orderby post.PostDate descending
                                 select post);
                    return query.ToList();
                }
                else
                {
                    var query = (from post in dataCoxtext.Posts.Include("Categories")
                                 where post.Categories.CategoryName == categoryName
                                 orderby post.PostDate descending
                                 select post);
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("ReadPostByCategory(" + categoryName + count + "):   " + ex.Message);
                return null;
            }
        }
        public static Image ReadImageById(Guid id)
        {
            try
            {
                var dataCoxtext = new DataContext();
                var query = (from image in dataCoxtext.Images
                            where image.ImageId == id
                            select image).First();
                return query;
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("ReadImageById(" + id + "):   " + ex.Message);
                return null;
            }
            
        }
        public static List<Image> ReadImagesByCategory(String categoryName, Int32 count)
        {
            try
            {
                var dataCoxtext = new DataContext();
                if (categoryName == "all")
                {
                    var query = (from image in dataCoxtext.Images.Include("Categories")
                                 where image.Categories.CategoryName != "post" && image.Categories.CategoryName != "avatar"
                                 select image).Take(count);
                    return query.ToList();
                }
                else
                {
                    var query = (from image in dataCoxtext.Images.Include("Categories")
                                 where image.Categories.CategoryName == categoryName
                                 select image).Take(count);
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("ReadImagesByCategory(" + categoryName+ ", " + count + "):   " + ex.Message);
                return null;
            }
            
        }
        public static Category ReadCategoryById(Guid categoryId)
        {
            try
            {
                var dataCoxtext = new DataContext();
                var query = (from item in dataCoxtext.Categories 
                            where item.CategoryId == categoryId
                            select item).First();
                return query;
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("ReadCategoryById(" + categoryId + "):   " + ex.Message);
                return null;
            }
        }
        public static List<Category> ReadCategories(String contentType)
        {
            try
            {
                var dataCoxtext = new DataContext();
                if (contentType == "all")
                {
                    var query = from category in dataCoxtext.Categories
                                where category.CategoryName != "avatar" && category.CategoryName != "post"
                                select category;
                    return query.ToList();
                }
                else
                {
                    var query = from category in dataCoxtext.Categories
                                where category.ContentType == contentType && category.CategoryName != "avatar" && category.CategoryName != "post"
                                select category;
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("ReadCategories(" + contentType + "):   " + ex.Message);
                return null;
            }
        }
        public static Comment ReadCommentById(Guid commentId)
        {
            try
            {
                var dataContext = new DataContext();
                var query = (from item in dataContext.Comments
                            where item.CommentId == commentId
                            select item).First();
                return query;
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("ReadCommentById(" + commentId + "):   " + ex.Message);
                return null;
            }
            
            
        }
        public static List<Comment> ReadCommentsByAuthorId(Guid authorId)
        {
            try
            {
                var dataContext = new DataContext();
                var query = from item in dataContext.Comments
                            where item.CommentAuthorId == authorId
                            orderby item.CommentDate descending
                            select item;
                return query.ToList();
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("ReadCommentsByAuthorId(" + authorId + "):   " + ex.Message);
                return null;
            }
            
        }
        public static List<Image> ReadImagesOfPostImage(Guid postId)
        {
            try
            {
                var dataContext = new DataContext();
                var query = from item in dataContext.PostImage.Include("Images")
                            where item.PostId == postId
                            select item.Images;
                return query.ToList();
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("ReadImagesOfPostImage(" + postId + "):   " + ex.Message);
                return null;
            }
        }
        public static List<Warning> ReadWarnings(Guid userId)
        {
            try
            {
                var dataContext = new DataContext();
                var query = from item in dataContext.Warnings
                            where item.UserId == userId
                            orderby item.Date descending
                            select item;
                return query.ToList();
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("ReadWarnings(" + userId + "):   " + ex.Message);
                return null;
            }
            
        }
        
        #endregion

        #region CREATE

        public static void CreatePost(Post post)
        {
            try
            {
                var dataCoxtext = new DataContext();
                dataCoxtext.Posts.AddObject(post);
                dataCoxtext.SaveChanges();
            }
            catch(Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("CreatePost():  " + ex.Message);
            }
            
        }
        public static void CreateComment(Comment comment)
        {
            try
            {
                var dataCoxtext = new DataContext();
                var profile = (from item in dataCoxtext.Profiles
                               where item.UserId == comment.CommentAuthorId
                               select item).First();
                var post = (from item in dataCoxtext.Posts
                            where item.PostId == comment.CommentPostId
                            select item).First();
                dataCoxtext.Comments.AddObject(comment);
                post.CommentCount++;
                profile.Comments++;
                dataCoxtext.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("CreateComment():  " + ex.Message);
            }
            
        }
        public static void CreateCategory(Category category)
        {
            try
            {
                var dataCoxtext = new DataContext();
                dataCoxtext.Categories.AddObject(category);
                dataCoxtext.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("CreateCategory():  " + ex.Message);
            }
            
        }
        public static void CreateProfile(Profile profile)
        {
            try
            {
                var dataCoxtext = new DataContext();
                dataCoxtext.Profiles.AddObject(profile);
                dataCoxtext.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("CreateProfile():  " + ex.Message);
            }
            
        }
        public static void CreateImage(Image image)
        {
            try
            {
                var dataCoxtext = new DataContext();
                dataCoxtext.Images.AddObject(image);
                dataCoxtext.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("CreateImage():  " + ex.Message);
            }
            
        }
        public static void CreateImageToPost(Image image, Guid postId)
        {
            try
            {
                var dataCoxtext = new DataContext();
                dataCoxtext.Images.AddObject(image);
                dataCoxtext.SaveChanges();
                dataCoxtext.PostImage.AddObject(new PostImage
                                                    {
                                                        PostImageId = Guid.NewGuid(),
                                                        ImageId = image.ImageId,
                                                        PostId = postId
                                                    });
                dataCoxtext.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("CreateImageToPost():  " + ex.Message);
            }
            
        }
        public static void CreateWarning(Warning warning)
        {
            try
            {
                var dataContext = new DataContext();
                dataContext.Warnings.AddObject(warning);
                dataContext.SaveChanges();
                AddWarningUser(warning.UserId);
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("CreateWarning():  " + ex.Message);
            }
            
        }

        #endregion

        #region DELETE

        public static void DeletePost(Guid postId)
        {
            try
            {
                var dataCoxtext = new DataContext();
                Post post = (from item in dataCoxtext.Posts.Include("Comments")
                             where item.PostId == postId
                             select item).First();
                foreach (var comment in post.Comments)
                {
                    if (!comment.aspnet_UsersReference.IsLoaded)
                    {
                        comment.aspnet_UsersReference.Load();
                        comment.aspnet_Users.ProfilesReference.Load();
                        comment.aspnet_Users.Profiles.Comments--;
                    }
                }
                dataCoxtext.DeleteObject(post);
                dataCoxtext.SaveChanges();
                DeletePostImageByPostId(post.PostId);
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("DeletePost():  " + ex.Message);
            }
            
        }
        public static void DeleteComment(Guid commentId)
        {
            try
            {
                var dataCoxtext = new DataContext();
                var query = (from item in dataCoxtext.Comments.Include("aspnet_Users")
                                   where item.CommentId == commentId
                                   select item).First();
                if (!query.aspnet_Users.ProfilesReference.IsLoaded)
                {
                    query.aspnet_Users.ProfilesReference.Load();
                }
                if (query.aspnet_Users.Profiles.Comments >0)
                {
                    query.aspnet_Users.Profiles.Comments--;
                }
                dataCoxtext.DeleteObject(query);
                dataCoxtext.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("DeleteComment():  " + ex.Message);
            }
            

        }
        public static void DeleteCategory(Guid categoryId)
        {
            try
            {
                var dataCoxtext = new DataContext();
                var query = (from item in dataCoxtext.Categories
                                     where item.CategoryId == categoryId
                                     select item).First();
                dataCoxtext.Categories.DeleteObject(query);
                dataCoxtext.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("DeleteCategory():  " + ex.Message);
            }
            
        }
        public static void DeleteImage(Guid imageId)
        {
            try
            {
                var dataCoxtext = new DataContext();
                var image = (from item in dataCoxtext.Images
                             where item.ImageId == imageId
                             select item).First();
                dataCoxtext.Images.DeleteObject(image);
                dataCoxtext.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("DeleteImage():  " + ex.Message);
            }
            
        }
        public static void DeletePostImageByImageId(Guid imageId)
        {
            try
            {
                var dataCoxtext = new DataContext();
                var query = (from item in dataCoxtext.PostImage
                             where item.ImageId == imageId
                             select item).First();
                dataCoxtext.PostImage.DeleteObject(query);
                dataCoxtext.SaveChanges();
                DeleteImage(imageId);
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("DeletePostImageByImageId():  " + ex.Message);
            }
            
        }
        public static void DeletePostImageByPostId(Guid postId)
        {
            try
            {
                var dataCoxtext = new DataContext();
                var query = (from item in dataCoxtext.PostImage
                             where item.PostId == postId
                             select item).ToList();
                for (int i = 0; i < query.Count(); i++)
                {
                    dataCoxtext.PostImage.DeleteObject(query[i]);
                }
                dataCoxtext.SaveChanges();
                for (int i = 0; i < query.Count(); i++)
                {
                    DeleteImage(query[i].ImageId);
                }
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("DeletePostImageByPostId():  " + ex.Message);
            }
            
            
        }
        public static void DeleteWarning(Guid warningId)
        {
            try
            {
                var dataContext = new DataContext();
                var query = (from item in dataContext.Warnings
                             where item.WarningId == warningId
                             select item).First();
                dataContext.Warnings.DeleteObject(query);
                dataContext.SaveChanges();
                DeleteWarningUser(query.UserId);
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("DeleteWarning():  " + ex.Message);
            }
            
        }

        #endregion

        #region UPDATE

        public static void UpdatePost(Post post)
        {
            try
            {
                var dataCoxtext = new DataContext();
                var query = (from item in dataCoxtext.Posts
                             where item.PostId == post.PostId
                             select item).First();

                query.PostTitle = post.PostTitle;
                query.PostText = post.PostText;
                query.PostExcerpt = post.PostExcerpt;
                query.PostCategoryId = post.PostCategoryId;
                query.CommentStatus = post.CommentStatus;
                query.PostDateModified = post.PostDateModified;
                dataCoxtext.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("UpdatePost():  " + ex.Message);
            }
            
        }
        public static void UpdateComment(Comment comment)
        {
            try
            {
                var dataCoxtext = new DataContext();
                var query = (from item in dataCoxtext.Comments
                             where item.CommentId == comment.CommentId && item.CommentAuthorId == comment.CommentAuthorId
                             select item).First();
                query.CommentText = comment.CommentText;
                dataCoxtext.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("UpdateComment():  " + ex.Message);
            }
            

        }
        public static void UpdateCategory(Category category)
        {
            try
            {
                var dataCoxtext = new DataContext();
                var query = (from item in dataCoxtext.Categories
                             where item.CategoryId == category.CategoryId
                             select item).First();
                query.CategoryName = category.CategoryName;
                query.CategoryDesc = category.CategoryDesc;
                query.ContentType = category.ContentType;
                dataCoxtext.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("UpdateCategory():  " + ex.Message);
            }
            
        }
        public static void UpdateProfile(Profile profile)
        {
            try
            {
                var dataCoxtext = new DataContext();
                var query = (from item in dataCoxtext.Profiles
                             where item.UserId == profile.UserId
                             select item).First();
                query.Avatar = profile.Avatar;
                query.Gender = profile.Gender;
                query.Age = profile.Age;
                query.Location = profile.Location;
                query.Autograph = profile.Autograph;
                dataCoxtext.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("UpdateProfile():  " + ex.Message);
            }
            
        }
        public static void UpdateImage(Image image)
        {
            try
            {
                var dataContext = new DataContext();
                var query = (from item in dataContext.Images 
                             where item.ImageId == image.ImageId 
                             select item).First();
                query.ImageCategoryId = image.ImageCategoryId;
                query.ImageDescription = image.ImageDescription;
                dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("UpdateImage():  " + ex.Message);
            }
            
        }

        #endregion

        #region Other

        public static void BanUser(Guid userId)
        {
            try
            {
                var dataContext = new DataContext();
                var query = (from profile in dataContext.Profiles
                             where profile.UserId == userId
                             select profile).First();
                query.IsBanned = true;
                query.Warnings = 0;
                dataContext.SaveChanges();
                var query2 = from item in dataContext.Warnings
                             where item.UserId == userId
                             select item;
                foreach (var warning in query2)
                {
                    DeleteWarning(warning.WarningId);
                }
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("BanUser():  " + ex.Message);
            }
            
        }
        public static void UnBanUser(Guid userId)
        {
            try
            {
                var dataContext = new DataContext();
                var query = (from profile in dataContext.Profiles
                             where profile.UserId == userId
                             select profile).First();
                query.IsBanned = false;
                dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("UnBanUser():  " + ex.Message);
            }
           
        }
        public static void AddWarningUser(Guid userId)
        {
            try
            {
                var dataContext = new DataContext();
                var query = (from profile in dataContext.Profiles
                             where profile.UserId == userId
                             select profile).First();
                if(query.Warnings >= 2)
                {
                    BanUser(userId);
                }
                else
                {
                    query.Warnings++;
                }
                dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("AddWarningUser():  " + ex.Message);
            }
            

            
        }
        public static void DeleteWarningUser(Guid userId)
        {
            try
            {
                var dataContext = new DataContext();
                var query = (from profile in dataContext.Profiles
                             where profile.UserId == userId
                             select profile).First();
                if (query.Warnings > 0)
                {
                    query.Warnings--;
                    dataContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("DeleteWarningUser():  " + ex.Message);
            }
            
        }
        public static Int32 GetPostsCount(String categoryName)
        {
            try
            {
                var dataCoxtext = new DataContext();
                if (categoryName == "all")
                {
                    var query = (from post in dataCoxtext.Posts
                                 select post).Count();
                    return query;
                }
                else
                {
                    var query = (from post in dataCoxtext.Posts.Include("Categories")
                                 where post.Categories.CategoryName == categoryName
                                 select post).Count();
                    return query;
                }
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("GetPostsCount(" + categoryName + "):  " + ex.Message);
                return 0;
            }
            
        }
        public static Int32 GetImagesCount(String categoryName)
        {
            try
            {
                var dataCoxtext = new DataContext();
                if (categoryName == "all")
                {
                    var query = (from image in dataCoxtext.Images
                                 select image).Count();
                    return query;
                }
                else
                {
                    var query = (from image in dataCoxtext.Images.Include("Categories")
                                 where image.Categories.CategoryName == categoryName
                                 select image).Count();
                    return query;
                }
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("GetImagesCount(" + categoryName + "):  " + ex.Message);
                return 0;
            }
            
        }
        public static List<Post> SearchPosts(String searchString)
        {
            try
            {
                var dataContext = new DataContext();
                var query = from post in dataContext.Posts
                            where post.PostText.Contains(searchString)
                            select post;
                return query.ToList();
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("SearchPosts(" + searchString + "):  " + ex.Message);
                return null;
            }
            
        }

        #endregion
    }
}
