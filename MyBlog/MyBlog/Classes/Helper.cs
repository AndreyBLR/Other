using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using MBPresentation.ServiceReference;

namespace MyBlog
{
    public static class Helper
    {
        public static List<Post> ChangeExcerpt(this List<Post> postList)
        {
            foreach (var post in postList)
            {
                post.PostExcerpt = Helper.ParseText(post.PostExcerpt);
            }
            return postList;
        }
        public static byte[] ScaleImage(byte[] image, int width, int height)
        {
            var stream = new MemoryStream();
            stream.Write(image, 0, image.Length);
            var source = new Bitmap(stream);
            {
                float srcwidth = source.Width;
                float srcheight = source.Height;
                byte[] result;
                if(width > source.Width && height > source.Height)
                {
                    stream = new MemoryStream();
                    source.Save(stream, ImageFormat.Jpeg);
                    result = stream.ToArray();
                    stream.Close();
                    return result;
                }
                var xRatio = width / srcwidth;
                var yRatio = height / srcheight;

                var ratio = (xRatio > yRatio) ? yRatio : xRatio;
                var useXRatio = (xRatio == ratio) ? true : false;

                var newWidth = useXRatio ? width : source.Width * ratio;
                var newHeight = !useXRatio ? height : source.Height * ratio;
                var bm = new Bitmap(Convert.ToInt32(newWidth), Convert.ToInt32(newHeight));
                var gr = Graphics.FromImage(bm);
                stream = new MemoryStream();
                gr.DrawImage(source, 0, 0, newWidth, newHeight);
                bm.Save(stream, ImageFormat.Jpeg);
                result = stream.ToArray();
                stream.Close();
                return result;
            }
        }
        public static bool IsSet(this Page page, String param)
        {
            if (page.Request.Params[param] != null)
            {
                
                return true;
            }
            else
            {
                return false;
            }
        }
        public static String ParseText (String text)
        {
            var imagePattern = new Regex(@"\[img=.{0,}]");
            var idImagePattern = new Regex(@"(\w{8}-\w{4}-\w{4}-\w{4}-\w{12})");
            var positionImagePattern = new Regex(@"pos='(\w{0,})'");
            while (imagePattern.IsMatch(text))
            {
                String style="";
                var image = imagePattern.Match(text);
                var idImage = idImagePattern.Match(image.Value);
                var positionImage = positionImagePattern.Match(image.Value);
                switch (positionImage.Groups[1].Value)
                {
                    case "Left":
                        style = "padding: 10px 10px 0px 0px;float:left;text-align:left";
                        break;
                    case "Right":
                        style = "padding: 0px 0px 10px 10px;float:right;text-align:right";
                        break;
                    case "Center":
                        style = "float:none;text-align:center";
                        break;
                }
                text = imagePattern.Replace(text, "<div style='" + style + "'><a href='Handlers/image.ashx?id=" + idImage.Value + "&mode=full' class='highslide' onclick='return hs.expand(this)'> <img src='Handlers/image.ashx?id=" + idImage.Value + "&mode=small' /> </a> <br /> <asp:Label runat='server' /> </div>", 1);
            }
            return text;
        }
        
    }

    
}
