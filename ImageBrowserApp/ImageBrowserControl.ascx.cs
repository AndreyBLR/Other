using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ImageBrowserApp
{
    public partial class ImageBrowserControl : System.Web.UI.UserControl
    {
        public int RowCount { get; set; }

        public int ColumnCount { get; set; }

        protected void Page_PreRender(object sender, EventArgs e)
        {        
        }
    }
}