using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using DataAccess;

namespace MvcApp.Models
{
    public class BookModel:IModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string AddInf { get; set; }
        public GenreModel Genre { get; set; }
        public FileModel File { get; set; }
        public IEnumerable<CommentModel> Comments { get; set; }

        public BookModel()
        {
            Id = Guid.NewGuid();
        }

        #region Implementation IModel members 

        public void Create()
        {
            var book = new Book
                           {
                               BookId = (Guid)Id,
                               BookName = Name,
                               BookAuthor = Author,
                               BookAddInf = AddInf,
                               Genre = (from genre in DataAccessManager.DataContext.Genres
                                        where genre.GenreId == Genre.Id
                                        select genre).FirstOrDefault()
                           };
            DataAccessManager.DataContext.Books.AddObject(book);
            DataAccessManager.DataContext.SaveChanges();
        }
        public void Read(Guid id)
        {
            var book =
                (from entity in DataAccessManager.DataContext.Books.Include("Genre").Include("Comments").Include("File")
                 where entity.BookId == id
                 select entity).FirstOrDefault();
            if (book != null)
            {
                Id = book.BookId;
                Name = book.BookName;
                Author = book.BookAuthor;
                AddInf = book.BookAddInf;
                File = new FileModel
                           {
                               Id = book.File.FileId,
                               Name = book.File.FileName,
                               Ext = book.File.FileType,
                               Bytes = book.File.FileBytes
                           };
                Genre = new GenreModel
                            {
                                Id = book.Genre.GenreId,
                                Name = book.Genre.GenreName,
                                Desc = book.Genre.GenreDescription
                            };
                Comments = from comment in book.Comments orderby comment.Date
                           select new CommentModel
                                      {
                                          Id = comment.CommentId,
                                          Text = comment.CommentText,
                                          Date = comment.Date,
                                          Book = this,
                                          User = new UserModel {Id = comment.UserId, NickName = Membership.GetUser(comment.UserId).UserName }
                                      };
            }
        }
        public void Update()
        {
            if(File != null)
            {
                var book = (from item in DataAccessManager.DataContext.Books 
                            where item.BookId == Id 
                            select item).FirstOrDefault();
                book.File = new File
                                {
                                    FileId = File.Id,
                                    FileName = File.Name,
                                    FileType = File.Ext,
                                    FileBytes = File.Bytes
                                };
                DataAccessManager.DataContext.SaveChanges();
            }
        }
        public void Delete()
        {
            var bookDb = (from book in DataAccessManager.DataContext.Books where book.BookId == Id select book).FirstOrDefault();
            if (bookDb != null)
            {
                DataAccessManager.DataContext.Books.DeleteObject(bookDb);
                DataAccessManager.DataContext.SaveChanges();
            }
        }

        #endregion
    }
        
}