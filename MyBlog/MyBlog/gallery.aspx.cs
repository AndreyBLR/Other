using System;
using System.Collections.Generic;
using System.Text;
using MBPresentation;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.Presenters.Page;
using MBPresentation.ServiceReference;

namespace MyBlog
{
    public partial class gallery : System.Web.UI.Page, IGalleryPage
    {
        private GalleryPagePresenter _presenter;

        public List<Image> ImageList { get; set; }
        public Int32 Count { get; set; }
        public String CategoryName { get; set; }
        public List<Tag> TagList { get; set; }

        public event VoidEventHandler ImagesReading;
        public event VoidEventHandler TagsReading;

        public gallery()
        {
            _presenter = new GalleryPagePresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
                  
            MakeHtmlTable();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if(this.IsSet("category"))
            {
                ReadImages(Request.Params["category"], 30);
                ReadTags();
                tagsCloud.TagList = TagList;
                tagsCloud.PageLink = "../gallery.aspx";
            }
            else
            {
                Response.Redirect("~/gallery.aspx?category=all");
            }
        }

        public string MakeHtmlTable()
        {
            if (ImageList != null)
            {
                var html = new StringBuilder();
                int i = 0;
                while (ImageList.Count > i)
                {
                    html.Append("<table width='100%' cellpadding='20px'>");
                    html.Append("<tr align='center'>");
                    int j;
                    for (j = i; j < i + 3; j++)
                    {
                        if (ImageList.Count > j)
                        {
                            html.Append("<td>");
                            html.Append("<table>");
                            html.Append("<tr>");
                            html.Append("<td>" +
                                    "<a href='Handlers/image.ashx?id=" + ImageList[j].ImageId +
                                    "&mode=full' class='highslide' onclick='return hs.expand(this)'> <img src='Handlers/image.ashx?id=" +
                                    ImageList[j].ImageId + "&mode=small' /> </a>" +
                                    "</td>");
                            html.Append("</tr>");
                            html.Append("<tr>");
                            html.Append("<td>" + ImageList[j].ImageDescription + "</td>");
                            html.Append("</tr>");
                            html.Append("<tr>");
                            html.Append("<td> <a href='gallery.aspx?category=" + ImageList[j].Categories.CategoryName + "'>" + ImageList[j].Categories.CategoryName + "</a> </td>");
                            html.Append("</tr>");
                            html.Append("</table>");
                        }
                        else
                        {
                            break;
                        }
                    }
                    i = j;
                    html.Append("</tr>");
                    html.Append("</table>");
                }
                return html.ToString();
            }
            return null;
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
        public void ReadTags()
        {
            if(TagsReading != null)
            {
                TagsReading();
            }
        }
    }
}

