using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApp.Models
{
    public class UserModel
    {
        public string NickName { get; set; }
        public Guid? Id { get; set; }
    }
}