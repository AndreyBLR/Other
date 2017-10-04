using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

namespace MvcApp.Models
{
    public class FileModel:IModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Ext { get; set; }
        public byte[] Bytes { get; set; }

        public FileModel()
        {
            Id = Guid.NewGuid();
        }

        #region Implementation IModel members

        public void Create()
        {
            var file = new File
                           {
                               FileId = Id, 
                               FileName = Name, 
                               FileType = Ext, 
                               FileBytes = Bytes
                           };

            DataAccessManager.DataContext.Files.AddObject(file);
            DataAccessManager.DataContext.SaveChanges();
        }
        public void Read(Guid id)
        {
            throw new NotImplementedException();
        }
        public void Update()
        {
            throw new NotImplementedException();
        }
        public void Delete()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}