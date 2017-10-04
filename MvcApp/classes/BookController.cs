using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataAccess;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class BookController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            if(TempData["searchresults"] != null)
            {
                return View(TempData["searchresults"]);
            }
            return View(DataAccessManager.ReadBooks());
        }
        public ActionResult ShowBook(string id)
        {
            var book = new BookModel();
            if(TempData["bookId"] != null)
            {
                book.Read((Guid)TempData["bookId"]);
                TempData.Clear();
                return View("Book", book);
            }
            else
            {
                book.Read(Guid.Parse(id));
                return View("Book", book);
            }
            
        }
        public ActionResult RedirectToBook()
        {
            var book = new BookModel();
            book.Read((Guid)TempData["redirectBookId"]);
            return View("Book", book);
        }
        public ActionResult DownloadBook(string id)
        {
            var book = new BookModel();
            book.Read(Guid.Parse(id));
            return File(book.File.Bytes, "text/plain", book.File.Name);
        }

        public ActionResult CreateComment(string userName, string bookId, string text)
        {
            if (userName != null)
            {
                var user = Membership.GetUser(userName);
                var comment = new CommentModel
                                  {
                                      User = new UserModel()
                                                 {
                                                     Id = (Guid) user.ProviderUserKey
                                                 },
                                      Text = text,
                                      Book = new BookModel() {Id = Guid.Parse(bookId)}
                                  };
                comment.Create();
            }
            TempData["redirectBookId"] = Guid.Parse(bookId);
            return RedirectToAction("RedirectToBook");
        }
        public ActionResult DeleteComment(string id)
        {
            var comment = new CommentModel() {Id = Guid.Parse(id)};
            comment.Read(Guid.Parse(id));
            var bookId = comment.Book.Id;
            comment.Delete();
            TempData["redirectBookId"] = bookId;
            return RedirectToAction("RedirectToBook");
        }
        public ActionResult UpdateComment(string id)
        {
            var comment = new CommentModel() { Id = Guid.Parse(id) };
            comment.Read(Guid.Parse(id));
            return View("ChangeComment", comment);
        }

        // POST: /Home/

        [HttpPost]
        public ActionResult UpdateComment(string id, string text)
        {
            var comment = new CommentModel();
            comment.Read(Guid.Parse(id));
            comment.Text = text;
            comment.Update();
            TempData["redirectBookId"] = comment.Book.Id;
            return RedirectToAction("RedirectToBook");
        }
    }
}
