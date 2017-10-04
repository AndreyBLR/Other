using System;
using System.Web.Security;
using MBPresentation;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.Presenters.Page;
using MBPresentation.ServiceReference;

namespace MyBlog
{
    public partial class registration : System.Web.UI.Page, IRegistrationPage
    {
        private RegistrationPagePresenter _presenter;

        public event VoidEventHandler ProfileCreating;

        public Profile UserProfile { get; set; }

        public registration()
        {
            _presenter = new RegistrationPagePresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/user/profile.aspx");
            }
        }

        protected void RegUserWizardCreatedUser(object sender, EventArgs e)
        {
            Roles.AddUserToRole(regUserWizard.UserName, "User");
            FormsAuthentication.Authenticate(regUserWizard.UserName, regUserWizard.Password);
            var profile = new Profile
                              {
                                  Age = "",
                                  Autograph = "",
                                  Gender = "",
                                  Location = "",
                                  Avatar = Guid.Parse("29fcd2b5-ebaf-43d8-9b10-77c29f6d8f68"),
                                  Comments = 0,
                                  Warnings = 0,
                                  UserId =
                                      Guid.Parse(Membership.GetUser(regUserWizard.UserName).ProviderUserKey.ToString())
                              };
            CreateProfile(profile);
        }

        private void CreateProfile(Profile profile)
        {
            UserProfile = profile;
            if(ProfileCreating != null)
            {
                ProfileCreating();
            }
        }
    }
}