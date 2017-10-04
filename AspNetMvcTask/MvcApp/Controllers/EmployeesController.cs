using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class EmployeesController : Controller
    {
        //
        // GET: /EmployeesTable/

        public ActionResult ShowEmployeeTable()
        {
            var model = new Model();
            return View("Employees", model.ReadEmployees());
        }

        //
        // POST: /EmployeesTable/

        [HttpPost]
        public ActionResult UpdateEmployee(Employee emp)
        {
            var model = new Model();
            if(emp != null)
            {
                model.UpdateEmployee(emp);   
            }
            return RedirectToAction("Default", "Default");
        }
        [HttpPost]
        public ActionResult DeleteEmployee(Guid id)
        {
            var model = new Model();
            model.DeleteEmployee(id);
            return RedirectToAction("Default", "Default");
        }
    }
}
