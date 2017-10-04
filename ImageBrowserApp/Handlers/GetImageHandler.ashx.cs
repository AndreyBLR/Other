using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using BusinessComponents;

namespace ImageBrowserApp
{
    /// <summary>
    /// Return the required picture
    /// that is specified in isThumb parameter
    /// </summary>
    public class Handler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            //check whether 'ID' parameter is not null
            if (context.Request.QueryString["ID"] == null)
            {
                return;
            }
            Guid id = Guid.Parse(context.Request.QueryString["ID"]);
            ImageComponent imageComponent = new ImageComponent();
            bool isThumb = false;
            //Check if client is asking for a thumbnail
            if (context.Request.QueryString["isThumb"] == null)
            {
                return;
            }
            try
            {
                isThumb = bool.Parse(context.Request.QueryString["isThumb"]);
            }
            catch (Exception e)
            {
                Logger.WriteErrorInLogFile(e.Message);
            }
            string imageFormat;
            if(isThumb)
            {
                byte[] thumbnailBytes = imageComponent.LoadThumbnailById(id, out imageFormat);
                context.Response.ContentType = "image/" + imageFormat;
                context.Response.BinaryWrite(thumbnailBytes);              
            }
            else
            {
                byte[] imageBytes = imageComponent.LoadImageById(id, out imageFormat);
                context.Response.ContentType = "image/" + imageFormat;
                context.Response.BinaryWrite(imageBytes);        
            }
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