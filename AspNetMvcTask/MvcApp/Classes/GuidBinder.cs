using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp.Classes
{
    public class GuidBinder:IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Guid id;
            var provider = bindingContext.ValueProvider;
            if(Guid.TryParse(provider.GetValue("Id").AttemptedValue, out id))
            {
                return id;
            }
            return Guid.Empty;
        }
    }
}