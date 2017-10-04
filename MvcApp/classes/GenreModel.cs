using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

namespace MvcApp.Models
{
    public class GenreModel: IModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

        public GenreModel()
        {
            Id = Guid.NewGuid();
        }

        #region Implementation IModel members

        public void Create()
        {
            var genreDb = new Genre
                              {
                                  GenreId = Id, 
                                  GenreName = Name, 
                                  GenreDescription = Desc
                              };
            DataAccessManager.DataContext.Genres.AddObject(genreDb);
            DataAccessManager.DataContext.SaveChanges();
        }
        public void Read(Guid id)
        {
            var genre = (from entity in DataAccessManager.DataContext.Genres
                         where entity.GenreId == id
                         select entity).FirstOrDefault();
            Id = id;
            Name = genre.GenreName;
            Desc = genre.GenreDescription;
        }
        public void Update()
        {
            var genreDb = (from genre in DataAccessManager.DataContext.Genres where genre.GenreId == Id select genre).FirstOrDefault();
            if(genreDb != null)
            {
                genreDb.GenreName = Name;
                genreDb.GenreDescription = Desc;
                DataAccessManager.DataContext.SaveChanges();
            }
        }
        public void Delete()
        {
            var genreDb = (from genre in DataAccessManager.DataContext.Genres where genre.GenreId == Id select genre).FirstOrDefault();
            if (genreDb != null)
            {
                DataAccessManager.DataContext.Genres.DeleteObject(genreDb);
                DataAccessManager.DataContext.SaveChanges();
            }
        }

        #endregion
    }
}