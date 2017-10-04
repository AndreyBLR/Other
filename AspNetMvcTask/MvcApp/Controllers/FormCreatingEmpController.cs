using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class FormCreatingEmpController : Controller
    {
        //
        // GET: /FormAddingEmp/

        public ActionResult ShowForm()
        {
            return View("FormCreatingEmp");
        }

        //
        // POST: /FormAddingEmp/
        [HttpPost]
        public ActionResult CreateEmployee(Employee emp)
        {
            var model = new Model();
            model.AddEmployee(emp);
            return RedirectToAction("ShowForm");
        }
    }
}
