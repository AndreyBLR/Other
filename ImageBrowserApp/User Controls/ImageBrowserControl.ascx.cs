using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessComponents;
using Interfaces;
using Microsoft.Practices.Unity;

namespace ImageBrowserApp
{
    public partial class ImageBrowserControl : System.Web.UI.UserControl
    {
        public int RowCount { get; set; }

        public int ColumnCount { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Define the name, type and url of the client script on the page.
            string csname = "name1",
                   csurl = ConfigurationManager.AppSettings["clientScriptURL"];
            Type cstype = this.GetType();

            // Get a ClientScriptManager reference from the Page class.
            ClientScriptManager cs = Page.ClientScript;
            if (!cs.IsClientScriptIncludeRegistered(cstype, csname))
            {
                cs.RegisterClientScriptInclude(cstype, csname, ResolveClientUrl(csurl));
            }
            IUnityContainer myContainer = new UnityContainer();
            myContainer.RegisterType<IImageDataAccessor, ImageComponent>();
            IImageDataAccessor imageComponent = myContainer.Resolve<IImageDataAccessor>();

            int totalImagesAmount = imageComponent.GetTotalAmountOfImages();
            string imageHandlerName = ConfigurationManager.AppSettings["imageHandlerName"],
                   imageInfoHandlerName = ConfigurationManager.AppSettings["imageInfoHandlerName"];

            if(Request.Cookies.AllKeys.Contains(UniqueID))
            {
                string[] array = Request.Cookies[UniqueID].Value.Split(new [] { "%2C" }, 2, StringSplitOptions.None);
                RowCount =Int32.Parse(array[0]);
                ColumnCount = Int32.Parse(array[1]);
            }
            //Building startup script
            string imageBrowserPageInitialization = String.Format(
                "var {0}Pager=new createImageBrowser('{1}'," +
                "'{2}'," +
                "'{3}'," +
                "'{4}'," +
                "'{5}'," +
                "'{6}'," +
                "'{7}'); {8}Pager.generateBrowser();",
                ImageBrowserContainer.ClientID,               
                ImageBrowserContainer.ClientID,
                UniqueID,
                RowCount,
                ColumnCount,
                totalImagesAmount,
                imageHandlerName,
                imageInfoHandlerName,
                ImageBrowserContainer.ClientID
                );

            string startupScript = string.Format("<script type='text/javascript'> {{ {0} }} </script>", imageBrowserPageInitialization);
            //Registrates script that initializes image browser.
            if (!Page.ClientScript.IsStartupScriptRegistered(this.GetType(), ImageBrowserContainer.UniqueID + "script"))
                Page.ClientScript.RegisterStartupScript(this.GetType(), ImageBrowserContainer.UniqueID + "script", startupScript);
        }
    }
}