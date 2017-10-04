using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Viewer
{
    public partial class Viewer : System.Web.UI.UserControl
    {
        public void BtnFileOpenClick(object sender, EventArgs e)
        {
            if (fileUpload.HasFile && fileUpload.PostedFile.ContentType == "text/plain")
            {
                var reader = new StreamReader(fileUpload.PostedFile.InputStream);
                inputContent.Text = reader.ReadToEnd();
                reader.Close();
            }
        }
    }
}