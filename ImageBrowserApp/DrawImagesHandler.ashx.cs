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
    /// Handler for creating JSON data
    /// </summary>
    public class DrawImagesHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            int rowCount= Int32.Parse(context.Request.QueryString["RowCount"]),
                columnCount = Int32.Parse(context.Request.QueryString["ColumnCount"]),
                pageNumber = Int32.Parse(context.Request.QueryString["PageNumber"]);
            StringBuilder stringBuilder=new StringBuilder();
            ImageComponent imageAccess=new ImageComponent();
            int beginRowNumber = rowCount*columnCount*(pageNumber - 1) + 1,
                endRowNumber=rowCount*columnCount*pageNumber;
          List<ImageInfo> imageInfoList = imageAccess.LoadListOfImagesInformation(beginRowNumber, endRowNumber);
          stringBuilder.Append("[");
            
            var serializer = new DataContractJsonSerializer(typeof (ImageInfo));
            for (int i = 0; i < imageInfoList.Count; i++)
            {
                using (var stream = new MemoryStream())
                {
                    serializer.WriteObject(stream, imageInfoList[i]);
                    stream.Position = 0;
                    stringBuilder.Append(new StreamReader(stream).ReadToEnd());
                    if (i < imageInfoList.Count - 1)
                    {
                        stringBuilder.Append(",");
                    }
                }
            }
            stringBuilder.Append("]");

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