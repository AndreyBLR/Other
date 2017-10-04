using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessComponents;
using Interfaces;
using Microsoft.Practices.Unity;
using Image = BusinessEntities.Image;

namespace ImageBrowserApp
{
    public partial class Default : System.Web.UI.Page
    {
        private static int _maxPictureWidth = 1024;
        private static int _maxPictureHeight = 768;
        private static int _maxThumbWidth = _maxPictureWidth / 6;
        private static int _maxThumbHeight = _maxPictureHeight / 6;

        /// <summary>
        /// Resize input picture according to _maxPictureWidth and _maxPictureHeight.
        /// </summary>
        /// <param name="inputBitmap">the input bitmap image</param>
        /// <param name="isThumb">boolean value that indicates whether
        /// the desired image is thumbnail</param>
        /// <returns></returns>
        private Bitmap GetImage(Bitmap inputBitmap, bool isThumb)
        {
            double ratio;
            int maxWidth = isThumb ? _maxThumbWidth : _maxPictureWidth,
                maxHeight = isThumb? _maxThumbHeight:_maxPictureHeight;
            int width = inputBitmap.Width, height = inputBitmap.Height;
            if ((width < maxWidth) && (height < maxHeight))
                return inputBitmap;  //image size is valid
            if ((width > maxWidth) && (height > maxHeight))
            {
                //choose minimum ratio in order to skew image size and keep sides correlation
                ratio = Math.Min((double)maxWidth / width, (double)maxHeight / height);
            }
            else
            {
                if ((width > maxWidth) && (height < maxHeight))
                    ratio = (double) maxWidth/width;
                else
                    ratio = (double) maxHeight/height;
            }

            //multiply both sizes in order to keep sides correlation
            Bitmap outputBitmap = new Bitmap(inputBitmap, (int)(width * ratio), (int)(height * ratio));

            return outputBitmap;
        }

        /// <summary>
        /// Get the appropriate image format
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns>the appropriate image format</returns>
        private ImageFormat GetImageFormat(string contentType)
        {
            return GetImageFormats()[contentType];
        }

        /// <summary>
        /// Initializes data structure used to define image format
        /// that corresponds to the content type of the uploaded image.
        /// </summary>
        private Dictionary<string, ImageFormat> GetImageFormats()
        {
            return new Dictionary<string, ImageFormat>()
                       {
                           {"image/jpg", ImageFormat.Jpeg},
                           {"image/jpeg", ImageFormat.Jpeg},
                           {"image/bmp", ImageFormat.Bmp},
                           {"image/png", ImageFormat.Png}
                       };
        }

        /// <summary>
        /// Go through child controls of
        /// the parent control and return them
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        private IEnumerable<Control> EnumerateControlsRecursive(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                yield return child;
                foreach (Control descendant in EnumerateControlsRecursive(child))
                    yield return descendant;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            List<string> userControlsUniqueIds=new List<string>();

            foreach (Control control in EnumerateControlsRecursive(ImageBrowsersPanel))
            {
                if (control is ImageBrowserControl)
                {
                    userControlsUniqueIds.Add(control.UniqueID);
                }
            }

            StringBuilder idArrayStringBuilder=new StringBuilder();
            idArrayStringBuilder.Append("new Array(");
            foreach (var userControlsUniqueId in userControlsUniqueIds)
            {
                idArrayStringBuilder.Append(String.Format("'{0}',", userControlsUniqueId));
            }
            //remove the last comma character
            idArrayStringBuilder.Remove(idArrayStringBuilder.Length - 1, 1);
            idArrayStringBuilder.Append(")");
            btnSave.OnClientClick = String.Format(
                "setupDimensionsOfAllImageBrowsers({0});",
                idArrayStringBuilder);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string contentType = imageFileUpload.PostedFile.ContentType;

            Bitmap imageForSaving = GetImage(new Bitmap(imageFileUpload.PostedFile.InputStream), false); //GetResizedImage(new Bitmap(imageFileUpload.PostedFile.InputStream));
            Bitmap thumbnailForSaving = GetImage(imageForSaving, true); //GetThumbnail(imageForSaving);

            MemoryStream stream = new MemoryStream();

            ImageFormat imageFormat = GetImageFormat(contentType);
            imageForSaving.Save(stream, imageFormat);
            byte[] imageBytes = stream.ToArray();

            stream.Close();

            stream = new MemoryStream();

            thumbnailForSaving.Save(stream, imageFormat);
            byte[] thumbnailBytes = stream.ToArray();

            stream.Close();

            ImageComponent imageComponent = new ImageComponent();
            imageComponent.SaveImage(new Image(Server.HtmlEncode(ImageNameTextBox.Text),
                Server.HtmlEncode(ImageDescriptionTextBox.Text), imageBytes, thumbnailBytes, imageFormat.ToString()));
            Page.Response.Redirect(Request.RawUrl);
        }          
    }
}