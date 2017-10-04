using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View("Admin");
        }
        public ActionResult Genres()
        {
            return View("Genres", DataAccessManager.ReadGenres());
        }
        public ActionResult CreateGenre()
        {
            return View("CreateGenre");
        }
        public ActionResult ChangeGenre(string id)
        {
            var genre = new GenreModel();
            genre.Read(Guid.Parse(id));
            return View("ChangeGenre", genre);
        }
        public ActionResult DeleteGenre(string id)
        {
            var genre = new GenreModel() { Id = Guid.Parse(id) };
            genre.Delete();
            return RedirectToAction("Genres");
        }


        public ActionResult CreateBook()
        {
            ViewData["Genres"] = DataAccessManager.ReadGenres();
            return View("CreateBook");
        }
        public ActionResult UploadBook()
        {
            if (TempData["bookId"] != null)
            {
                ViewData["bookId"] = TempData["bookId"];
                ViewData["Genres"] = DataAccessManager.ReadGenres();
                return View("UploadBook");
            }

            return View("Admin");
        }
        public ActionResult DeleteBook(string id)
        {
            var book = new BookModel() { Id = Guid.Parse(id) };
            book.Delete();
            return RedirectToAction("Index", "Book");
        }
        
        // POST: /Admin/

        [HttpPost]
        public ActionResult CreateGenre(GenreModel genre)
        {
            genre.Create();
            return RedirectToAction("Genres");
        }
        [HttpPost]
        public ActionResult ChangeGenre(string id, string name, string desc)
        {
            var genre = new GenreModel() { Id = Guid.Parse(id), Name = name, Desc = desc };
            genre.Update();
            return RedirectToAction("Genres");
        }

        [HttpPost]
        public ActionResult CreateBook(BookModel book)
        {
            book.Create();
            TempData["bookId"] = book.Id;
            return RedirectToAction("UploadBook");
        }
        [HttpPost]
        public ActionResult UploadBook(string id, HttpPostedFileBase uploadFile)
        {
            if(!String.IsNullOrEmpty(id))
            {
                byte[] file = new byte[uploadFile.InputStream.Length];
                uploadFile.InputStream.Read(file, 0, (int)uploadFile.InputStream.Length);
                var book = new BookModel
                {
                    Id = Guid.Parse(id),
                    File = new FileModel()
                    {
                        Bytes = file,
                        Name = uploadFile.FileName
                    }
                };
                book.Update();
                TempData["bookId"] = book.Id;
                return RedirectToAction("ShowBook", "Book");
            }
            
            return RedirectToAction("Index");
        }

        
        
    }
}
