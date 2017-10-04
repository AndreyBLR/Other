using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

namespace MvcApp.Models
{
    public class CommentModel: IModel
    {
        public Guid Id { get; set; }
        public UserModel User { get; set; }
        public string Text { get; set; }
        public DateTime? Date { get; set; }
        public BookModel Book { get; set; }

        public CommentModel()
        {
            Id = Guid.NewGuid();
        }

        #region Implementation IModel members

        public void Create()
        {
            var comment = new Comment
                              {
                                  CommentText = Text,
                                  CommentId = Id,
                                  aspnet_Users = (from user in DataAccessManager.DataContext.aspnet_Users
                                                  where user.UserId == User.Id
                                                  select user).FirstOrDefault(),
                                  Book = (from book in DataAccessManager.DataContext.Books
                                          where book.BookId == Book.Id
                                          select book).FirstOrDefault(),
                                  Date = DateTime.Now
                              };
            DataAccessManager.DataContext.Comments.AddObject(comment);
            DataAccessManager.DataContext.SaveChanges();
        }
        public void Read(Guid id)
        {
            var commentDb =
                (from entity in DataAccessManager.DataContext.Comments.Include("Book")
                 where entity.CommentId == id
                 select entity).FirstOrDefault();
            if (commentDb != null)
            {
                Id = commentDb.CommentId;
                Text = commentDb.CommentText;
                Book = new BookModel(){Id=commentDb.BookId};
            }
        }
        public void Update()
        {
            var commentDb = (from item in DataAccessManager.DataContext.Comments
                             where item.CommentId == Id
                             select item).FirstOrDefault();
            if(commentDb != null)
            {
                commentDb.CommentText = Text;
                DataAccessManager.DataContext.SaveChanges();
            }
            

        }

        public void Delete()
        {
            var commentDb = (from comment in DataAccessManager.DataContext.Comments where comment.CommentId == Id select comment).FirstOrDefault();
            if (commentDb != null)
            {
                DataAccessManager.DataContext.Comments.DeleteObject(commentDb);
                DataAccessManager.DataContext.SaveChanges();
            }
        }

        #endregion
    }
}