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
            Guid id = Guid.Parse(context.Request.QueryString["ID"]);
            ImageComponent imageAccess = new ImageComponent();
            context.Response.BinaryWrite(
                imageAccess.LoadImageById(id));
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