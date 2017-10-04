using System;
using System.Web;
using Classes;
using MBPresentation;
using MBPresentation.Interfaces.ViewInterfaces.Handler;
using MBPresentation.Presenters.Handler;
using MBPresentation.ServiceReference;

namespace MyBlog.Handlers
{
    /// <summary>
    /// Summary description for image
    /// </summary>
    public class image : IHttpHandler, IImageHttpHandler
    {
        private ImageHttpHandlerPresenter _presenter;

        public event VoidEventHandler ImageReading;

        public Guid ImageId { get;set; }
        public Image Image { get; set; }

        public image()
        {
            _presenter = new ImageHttpHandlerPresenter(this);
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                ImageId = Guid.Parse(context.Request.Params["id"]);
                ImageReading();
                if (context.Request.Params["mode"] == "small")
                {
                    context.Response.OutputStream.Write(Image.ImageSmall, 0, Image.ImageSmall.Length);
                }
                if (context.Request.Params["mode"] == "full")
                {
                    context.Response.OutputStream.Write(Image.ImageFile, 0, Image.ImageFile.Length);
                }
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error(ex);
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