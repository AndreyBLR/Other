using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using Classes;
using MBPresentation;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.Presenters.Page;
using MBPresentation.ServiceReference;

namespace MyBlog.admin
{
    public partial class users : System.Web.UI.Page, IUsersPage
    {
        private UsersPagePresenter _presenter;

        public event VoidEventHandler UserBanning;
        public event VoidEventHandler UserUnbannig;
        public event VoidEventHandler WarningAdding;
        public event VoidEventHandler WarningDeleting;
        public event VoidEventHandler UserReading;
        public event VoidEventHandler UserStatusReading;

        public Guid UserId { get; set; }
        public Profile UserProfile { get; set; }
        public bool IsBanned { get; set; }

        public users()
        {
            _presenter = new UsersPagePresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (this.IsSet("action") && this.IsSet("id"))
                {
                    Guid id;
                    if (Guid.TryParse(Request.Params["id"], out id))
                    {
                        switch (Request.Params["action"])
                        {
                            case "ban":
                                BanUser(id);
                                Response.Redirect(Request.UrlReferrer.AbsoluteUri);
                                break;
                            case "unban":
                                UnbanUser(id);
                                Response.Redirect(Request.UrlReferrer.AbsoluteUri);
                                break;
                            case "delete":
                                Membership.DeleteUser(Membership.GetUser(id).UserName);
                                Response.Redirect(Request.UrlReferrer.AbsoluteUri);
                                break;
                            default:
                                Response.Redirect("~/admin/users.aspx");
                                break;
                        }
                    }
                }
                else
                {
                    multiView.SetActiveView(usersView);
                    var users = Membership.GetAllUsers();
                    lstUsers.DataSource = users;
                    lstUsers.DataBind();
                }
            }
            catch(Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("admin/users.aspx   " + User.Identity.Name + "   " + ex.Message);
                Response.Redirect("~/admin/users.aspx");
            }
            
        }

        public void ReadUserStatus(Guid userId)
        {
            UserId = userId;
            if(UserStatusReading != null)
            {
                UserStatusReading();
            }
        }
        private void BanUser(Guid userId)
        {
            UserId = userId;
            if(UserBanning != null)
            {
                UserBanning();
            }
        }
        private void UnbanUser(Guid userId)
        {
            UserId = userId;
            if (UserUnbannig != null)
            {
                UserUnbannig();
            }
        }
        
        protected void LstUsersItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                var user = (MembershipUser)e.Item.DataItem;
                var lnk = (HyperLink)e.Item.FindControl("lnkBanUser");
                if (user.ProviderUserKey != null)
                {
                    ReadUserStatus((Guid)user.ProviderUserKey);
                    if (IsBanned)
                    {
                        lnk.Text = "Unban";
                        lnk.NavigateUrl = "~/admin/users.aspx?id=" + user.ProviderUserKey + "&action=unban";
                    }
                    else
                    {
                        lnk.Text = "Ban";
                        lnk.NavigateUrl = "~/admin/users.aspx?id=" + user.ProviderUserKey + "&action=ban";
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error("admin/users.aspx   " + User.Identity.Name + "   " + ex.Message);
                Response.Redirect("~/admin/users.aspx");
            }
        }
    }
}