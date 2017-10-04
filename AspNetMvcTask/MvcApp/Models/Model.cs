using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MvcApp.Models.DataAccess;

namespace MvcApp.Models
{
    public class Model
    {
        private DataAccessManager _manager;
        public Model()
        {
            _manager = new DataAccessManager(ConfigurationManager.ConnectionStrings["EmployeesDB"].ToString());
        }
        public void AddEmployee(Employee emp)
        {
            _manager.AddEmployee(emp);
        }
        public List<Employee> ReadEmployees()
        {
            return _manager.ReadEmployees();
        }
        public void UpdateEmployee(Employee emp)
        {
            _manager.UpdateEmployee(emp);
        }
        public void DeleteEmployee(Guid id)
        {
            _manager.DeleteEmployee(id);
        }
    }
}