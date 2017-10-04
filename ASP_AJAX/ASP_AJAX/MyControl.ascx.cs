using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP_AJAX
{
    public partial class MyControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["message"] != null)
            {
                    Context.Response.Write(Request.Params["message"]);
                    Context.Response.End();
            }
        }
    }
}