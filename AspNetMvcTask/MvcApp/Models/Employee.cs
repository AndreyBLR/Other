using System;

namespace MvcApp.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Birthday { get; set; }
        public String DayOfEmployment { get; set; }
    }
}