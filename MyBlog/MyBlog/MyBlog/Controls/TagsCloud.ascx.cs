using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MBPresentation;

namespace MyBlog.Controls
{
    public partial class TagsCloud : System.Web.UI.UserControl
    {
        public List<Tag> TagList { get; set; }
        public String PageLink { get; set; }
        public Int32 Count { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (TagList != null && TagList.Count > 0)
            {
                double max = Convert.ToDouble(TagList.Max(item => item.Count));

                int m = 0;
                foreach (Tag tag in TagList)
                {
                    double part = 0;
                    if (max != 0)
                    {
                        part = (Convert.ToDouble(tag.Count)/max)*100;
                    }
                    else
                    {
                        part = 10;
                    }

                    int fontsize = Convert.ToInt32(part/10)*2;
                    tagsCloud.Text += "<a href='" + PageLink.ToString() + "?category=" + tag.TagName.ToString()+"'><SPAN style='color:#F5F3C3; font-size:" + fontsize + "pt'>" + tag.TagName.ToString() + "</SPAN><SUB><SPAN style='color:#FFEEFF; font-size:7pt;'>" + tag.Count.ToString() + "</SPAN></SUB></a> ";
                } 
            }
        }
    }
}