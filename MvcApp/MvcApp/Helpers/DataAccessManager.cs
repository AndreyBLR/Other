using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcApp;
using MvcApp.Models;

namespace DataAccess
{
    public static class DataAccessManager
    {
        private static DBEntities _dataContext = new DBEntities();

        public static DBEntities DataContext
        {
            get { return _dataContext; }
        }

        public static IEnumerable<GenreModel> ReadGenres()
        {
            IEnumerable<GenreModel> genres = (from item in _dataContext.Genres
                                              select new GenreModel
                                                         {
                                                             Id = item.GenreId,
                                                             Name = item.GenreName,
                                                             Desc = item.GenreDescription

                                                         });
            return genres;
        }
        public static IEnumerable<BookModel> ReadBooks()
        {
            return from item in _dataContext.Books.Include("Genres").Include("Files") select new BookModel
                                                               {
                                                                   Id = item.BookId,
                                                                   Name = item.BookName,
                                                                   Author = item.BookAuthor,
                                                                   AddInf = item.BookAddInf,
                                                                   Genre = new GenreModel
                                                                               {
                                                                                   Name = item.Genre.GenreName
                                                                               },
                                                                   File = new FileModel
                                                                              {
                                                                                  Id = item.File.FileId, 
                                                                                  Name = item.File.FileName, 
                                                                                  Ext = item.File.FileType, 
                                                                                  Bytes = item.File.FileBytes
                                                                              }
                                                               };
        }

        public static IEnumerable<BookModel> ReadBooks(Guid genreId)
        {
            return from item in _dataContext.Books.Include("Genres").Include("Files") 
                   where item.GenreId == genreId
                   select new BookModel
                   {
                       Id = item.BookId,
                       Name = item.BookName,
                       Author = item.BookAuthor,
                       AddInf = item.BookAddInf,
                       Genre = new GenreModel
                       {
                           Name = item.Genre.GenreName
                       },
                       File = new FileModel
                       {
                           Id = item.File.FileId,
                           Name = item.File.FileName,
                           Ext = item.File.FileType,
                           Bytes = item.File.FileBytes
                       }
                   };
        }
        public static IEnumerable<BookModel> ReadBooks(string name)
        {
            return from item in _dataContext.Books.Include("Genres").Include("Files")
                   where item.BookName.Contains(name)
                   select new BookModel
                   {
                       Id = item.BookId,
                       Name = item.BookName,
                       Author = item.BookAuthor,
                       AddInf = item.BookAddInf,
                       Genre = new GenreModel
                       {
                           Name = item.Genre.GenreName
                       },
                       File = new FileModel
                       {
                           Id = item.File.FileId,
                           Name = item.File.FileName,
                           Ext = item.File.FileType,
                           Bytes = item.File.FileBytes
                       }
                   };
        }
        public static IEnumerable<BookModel> ReadBooks(Guid genreId, string name)
        {
            return from item in _dataContext.Books.Include("Genres").Include("Files")
                   where item.BookName.Contains(name) && item.GenreId == genreId
                   select new BookModel
                   {
                       Id = item.BookId,
                       Name = item.BookName,
                       Author = item.BookAuthor,
                       AddInf = item.BookAddInf,
                       Genre = new GenreModel
                       {
                           Name = item.Genre.GenreName
                       },
                       File = new FileModel
                       {
                           Id = item.File.FileId,
                           Name = item.File.FileName,
                           Ext = item.File.FileType,
                           Bytes = item.File.FileBytes
                       }
                   };
        }

    }
}

