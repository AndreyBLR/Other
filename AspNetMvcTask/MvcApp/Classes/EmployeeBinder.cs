using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models;

namespace MvcApp.Classes
{
    public class EmployeeBinder:IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Employee result = null;
            var datePattern = new Regex(@"^\d{2}\/\d{2}\/\d{4}");
            var provider = bindingContext.ValueProvider;
            if(!String.IsNullOrEmpty(provider.GetValue("Name").AttemptedValue) && datePattern.IsMatch(provider.GetValue("Birthday").AttemptedValue) &&datePattern.IsMatch(provider.GetValue("DayOfEmployment").AttemptedValue))
            {
                result = new Employee
                             {
                                 Name = provider.GetValue("Name").AttemptedValue,
                                 Birthday = provider.GetValue("Birthday").AttemptedValue,
                                 DayOfEmployment = provider.GetValue("DayOfEmployment").AttemptedValue
                             };
            }
            if(provider.GetValue("Id") != null && result != null)
            {
                result.Id = Guid.Parse(provider.GetValue("Id").AttemptedValue);
            }
            return result;
        }
    }
}