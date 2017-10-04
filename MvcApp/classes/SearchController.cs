using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Index()
        {
            ViewData["Genres"] = DataAccessManager.ReadGenres();
            return View("Search");
        }

        // POST: /Search/

        [HttpPost]
        public ActionResult Search(string genre, string name)
        {
            IEnumerable<BookModel> books = new List<BookModel>();
            if (genre != null && name != string.Empty)
            {
                books = DataAccessManager.ReadBooks(Guid.Parse(genre), name);
            }
            else
            {
                if (genre == null)
                {
                    books = DataAccessManager.ReadBooks(name);
                }
                if (name == string.Empty)
                {
                    books = DataAccessManager.ReadBooks(Guid.Parse(genre));
                }
            }
            TempData["searchresults"] = books;
            return RedirectToAction("Index", "Book");
        }

    }
}
