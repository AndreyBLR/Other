using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessComponents;

namespace ImageBrowserApp
{
    /// <summary>
    /// Summary description for Handler
    /// </summary>
    public class Handler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString["ID"] == null)
            {
                return;
            }
            ImageAccess imageAccess = new ImageAccess();
            context.Response.BinaryWrite(
                imageAccess.LoadImages(new Dictionary<string, object>() { { "id", context.Request.QueryString["ID"] } })[0].
                    ImageBytes);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}