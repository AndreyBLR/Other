using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.Presenters.Page;
using MBPresentation.ServiceReference;

namespace MyBlog.admin
{
    public partial class gallery : System.Web.UI.Page, IAdminGalleryPage
    {
        private AdminGalleryPagePresenter _presenter;

        public List<Image> ImageList { get; set; }
        public Image Image { get; set; }
        public Guid ImageId { get; set; }
        public List<Category> CategoryList { get; set; }
        public string CategoryName { get; set; }
        public int Count { get; set; }

        public event VoidEventHandler ImageReading;
        public event VoidEventHandler ImageCreating;
        public event VoidEventHandler ImageDeleting;
        public event VoidEventHandler ImagesReading;
        public event VoidEventHandler ImageUpdating;
        public event VoidEventHandler CategoriesReading;

        public gallery()
        {
            _presenter = new AdminGalleryPagePresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                ReadCategories();
                Cache.Insert("CategoryList", CategoryList, null, DateTime.Now.AddMinutes(5d), System.Web.Caching.Cache.NoSlidingExpiration);
                return;
            }
            if(Request.UrlReferrer.LocalPath != "/admin/gallery.aspx" || Cache["CategoryList"] == null)
            {
                ReadCategories();
                Cache.Insert("CategoryList", CategoryList, null, DateTime.Now.AddMinutes(5d), System.Web.Caching.Cache.NoSlidingExpiration);
                return;
            }
        }
        protected void BtnSaveClick(object sender, EventArgs e)
        {
            var image = (Image)Cache["Image"];
            image.ImageCategoryId = (from category in (List<Category>) Cache["CategoryList"]
                                     where category.CategoryName == ddlCategoryList.SelectedItem.Text
                                     select category.CategoryId).First();
            image.ImageDescription = txbDesc.Text;
            UpdateImage(image);
            Response.Redirect("~/admin/gallery.aspx?category=all");
        }
        protected void LoadBtnClick(object sender, EventArgs e)
        {
            var buf = new byte[fileUpload.PostedFile.InputStream.Length];
            fileUpload.PostedFile.InputStream.Read(buf, 0, Convert.ToInt32(fileUpload.PostedFile.InputStream.Length));
            var image = new Image
                            {
                                ImageId = Guid.NewGuid(),
                                ImageSmall = Helper.ScaleImage(buf, 156, 120),
                                ImageFile = Helper.ScaleImage(buf, 700, 540),
                                ImageCategoryId = (from category in (List<Category>) Cache["CategoryList"]
                                                   where category.CategoryName == ddlCategoryList.SelectedItem.Text
                                                   select category.CategoryId).First(),
                                                 
                                ImageRating = "0",
                                ImageDate = DateTime.Now,
                                ImageDescription = txbDesc.Text
                            };
            CreateImage(image);
            Response.Redirect("~/admin/gallery.aspx");
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (this.IsSet("action"))
            {
                switch (Request.Params["action"])
                {
                    case "add":
                        multiView.SetActiveView(formView);
                        ddlCategoryList.DataSource = from item in (List<Category>)Cache["CategoryList"] select item.CategoryName;
                        ddlCategoryList.DataBind();
                        btnSave.Visible = false;
                        break;
                    case "delete":
                        if (this.IsSet("id"))
                        {
                            DeleteImage(Guid.Parse(Request.Params["id"]));
                        }
                        Response.Redirect("~/admin/gallery.aspx?category=all");
                        break;
                    case "change":
                        Guid id;
                        if (this.IsSet("id") && Guid.TryParse(Request.Params["id"], out id))
                        {
                            ReadImage(id);
                            Cache.Insert("Image", Image);
                            ddlCategoryList.DataSource = from item in (List<Category>)Cache["CategoryList"] select item.CategoryName;
                            ddlCategoryList.DataBind();
                            multiView.SetActiveView(formView);
                            plLoadForm.Visible = false;
                            txbDesc.Text = Image.ImageDescription;
                        }
                        else
                        {
                            Response.Redirect("~/Admin/gallery.aspx?category=all");
                        }
                        break;
                    default:
                        Response.Redirect("~/admin/gallery.aspx?category=all");
                        break;
                }   
            }
            else
            {
                if (this.IsSet("category"))
                {
                    Cache.Remove("images");
                    multiView.SetActiveView(imagesView);
                    ReadImages(Request.Params["category"], 25);
                }
                else
                {
                    Response.Redirect("~/admin/gallery.aspx?category=all");
                }
            }
        }

        public string MakeHtmlTable()
        {
            if (ImageList == null)
            {
                return null;
            }
            var html = new StringBuilder();
            var i = 0;
            while (ImageList.Count > i)
            {
                html.Append("<table width='100%'>");
                html.Append("<tr align='center'>");
                int j;
                for (j = i; j < i + 3; j++)
                {
                    if (ImageList.Count <= j)
                    {
                        break;
                    }
                    html.Append("<td>");
                    html.Append("<table cellpadding='10px'>");
                    html.Append("<tr>");
                    html.Append("<td> <img src='../Handlers/image.ashx?id=" + ImageList[j].ImageId + "&mode=small' /> </td>");
                    html.Append("</tr>");
                    html.Append("<tr>");
                    html.Append("<td>Category: " + ImageList[j].Categories.CategoryName + "</td>");
                    html.Append("</tr>");
                    html.Append("<tr>");
                    html.Append("<td>" +
                                "<table width='100%'><tr><td align='center'><a href='../admin/gallery.aspx?id=" +
                                ImageList[j].ImageId +
                                "&action=delete'>Delete</a></td> <td align='center'><a href='../admin/gallery.aspx?id=" +
                                ImageList[j].ImageId + "&action=change'>Change</a></td> </tr></table>" +
                                "</td>");
                    html.Append("</tr>");
                    html.Append("</table>");
                }
                i = j;
                html.Append("</tr>");
                html.Append("</table>");
            }
            return html.ToString();
        }

        public void ReadImage(Guid imageId)
        {
            ImageId = imageId;
            if(ImageReading != null)
            {
                ImageReading();
            }
        }
        public void CreateImage(Image image)
        {
            Image = image;
            if(ImageCreating != null)
            {
                ImageCreating();
            }
        }
        public void UpdateImage(Image image)
        {
            Image = image;
            if(ImageUpdating != null)
            {
                ImageUpdating();
            }
        }
        public void DeleteImage(Guid imageId)
        {
            ImageId = imageId;
            if(ImageDeleting != null)
            {
                ImageDeleting();
            }
        }
        public void ReadImages(String categoryName, Int32 count)
        {
            CategoryName = categoryName;
            Count = count;
            if(ImagesReading != null)
            {
                ImagesReading();
            }
        }
        public void ReadCategories()
        {
            if(CategoriesReading != null)
            {
                CategoriesReading();
            }
        }
    }
}