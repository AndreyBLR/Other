using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using BusinessEntities;
using BusinessComponents;

namespace ImageBrowserApp.Handlers
{
    /// <summary>
    /// Summary description for DrawImagesHandler
    /// </summary>
    public class DrawImagesHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int rowCount= Int32.Parse(context.Request.QueryString["RowCount"]),
                columnCount = Int32.Parse(context.Request.QueryString["ColumnCount"]),
                pageNumber = Int32.Parse(context.Request.QueryString["PageNumber"]);
            StringBuilder stringBuilder=new StringBuilder();
            ImageAccess imageAccess=new ImageAccess();
            int beginRowNumber = rowCount*columnCount*(pageNumber - 1) + 1,
                endRowNumber=rowCount*columnCount*pageNumber;
          List<Image> images = imageAccess.LoadImages(beginRowNumber, endRowNumber);
          stringBuilder.Append("[");
            foreach (var image in images)
            {
                stringBuilder.Append("{");
                stringBuilder.Append(String.Format("\"Id\":\"{0}\",", image.Id));
                stringBuilder.Append(String.Format("\"Name\":\"{0}\",", image.Name));
                stringBuilder.Append(String.Format("\"Description\":\"{0}\",", image.Description));
                stringBuilder.Append("}");
            }
            stringBuilder.Append("]");
       //     HtmlTextWriter writer=new HtmlTextWriter(context.);
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(stringBuilder.ToString());
            context.Response.End();
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