using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using MBPresentation;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.Presenters.Page;
using MBPresentation.ServiceReference;
using Image = MBPresentation.ServiceReference.Image;

namespace MyBlog.user
{
    public partial class profile : System.Web.UI.Page, IProfilePage
    {
        private ProfilePagePresenter _presenter;

        public event VoidEventHandler ProfileReading;
        public event VoidEventHandler ProfileUpdating;
        public event VoidEventHandler ImageCreating;

        public Guid UserId { get; set; }
        public Profile UserProfile { get; set; }
        public Image Avatar { get; set; }
        private List<Profile> list;

        public profile()
        {
            _presenter = new ProfilePagePresenter(this);
            list = new List<Profile>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SaveBtnClick(object sender, EventArgs e)
        {
            ReadProfile((Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey);
            TextBox txb = null;
            txb = (TextBox)lstUserProfile.Controls[0].FindControl("gender");
            UserProfile.Gender = txb.Text;
            txb = (TextBox)lstUserProfile.Controls[0].FindControl("age");
            UserProfile.Age = txb.Text;
            txb = (TextBox)lstUserProfile.Controls[0].FindControl("location");
            UserProfile.Location = txb.Text;
            txb = (TextBox)lstUserProfile.Controls[0].FindControl("autograph");
            UserProfile.Autograph = txb.Text;
            UpdateProfile(UserProfile);
        }

        protected void uploadBtnClick(object sender, EventArgs e)
        {
            FileUpload fileUpload = (FileUpload) lstUserProfile.Controls[0].FindControl("fileUpload");

            if ((fileUpload.PostedFile.ContentLength < 8388608) && (fileUpload.PostedFile.ContentType == "image/jpg" || fileUpload.PostedFile.ContentType == "image/jpeg"))
            {
                byte[] buf = new byte[fileUpload.PostedFile.InputStream.Length];
                fileUpload.PostedFile.InputStream.Read(buf, 0, Convert.ToInt32(fileUpload.PostedFile.InputStream.Length));
                ReadProfile((Guid) Membership.GetUser(User.Identity.Name).ProviderUserKey);
                var image = new Image
                                {
                                    ImageId = Guid.NewGuid(),
                                    ImageFile = Helper.ScaleImage(buf, 133, 100),
                                    ImageCategoryId = Guid.Parse("34020e34-8835-40bb-936a-542baa8b2677")
                                };
                CreateAvatar(image);
                UserProfile.Avatar = image.ImageId;
                UpdateProfile(UserProfile);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Helper.IsSet(this, "userName"))
            {
                if (Request.Params["userName"] != User.Identity.Name)
                {
                    ReadProfile((Guid)Membership.GetUser(Request.Params["userName"]).ProviderUserKey);
                    list.Add(UserProfile);
                    multiView.SetActiveView(viewUserProfile);
                    lstProfile.DataSource = list;
                    lstProfile.DataBind();
                }
                else
                {
                    Response.Redirect("~/user/profile.aspx");
                }
            }
            else
            {
                if (User.Identity.IsAuthenticated)
                {
                    ReadProfile((Guid) Membership.GetUser(User.Identity.Name).ProviderUserKey);
                    multiView.SetActiveView(viewChangeUserProfile);
                    list.Add(UserProfile);
                    lstUserProfile.DataSource = list;
                    lstUserProfile.DataBind();
                }
                else
                {
                    Response.Redirect("~/default.aspx");
                }
            }
        }

        private void CreateAvatar(Image userAvatar)
        {
            Avatar = userAvatar;
            if(ImageCreating != null)
            {
                ImageCreating();
            }
        }
        private void ReadProfile(Guid userId)
        {
            UserId = userId;
            if (ProfileReading != null)
            {
                ProfileReading();
            }
        }
        private void UpdateProfile(Profile profile)
        {
            UserProfile = profile;
            if(ProfileUpdating != null)
            {
                ProfileUpdating();
            }
        }
    }
}