using System;
using System.Collections.Generic;
using System.Web.Security;
using Classes;
using MBPresentation;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.Presenters.Page;
using MBPresentation.ServiceReference;

namespace MyBlog.user
{
    public partial class warnings : System.Web.UI.Page, IWarningsPage
    {
        private WarningsPagePresenter _presenter;

        public event VoidEventHandler UserStatusReading;
        public List<Warning> WarningList { get; set; }
        public Guid UserId { get; set; }
        public Guid WarningId { get; set; }
        public Warning Warning { get; set; }
        public bool IsBanned { get; set; }

        public event VoidEventHandler WarningsReading;
        public event VoidEventHandler WarningAdding;
        public event VoidEventHandler WarningDeleting;

        public warnings()
        {
            _presenter = new WarningsPagePresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddBtnClick(object sender, EventArgs e)
        {
            var warning = new Warning
                              {
                                  WarningId = Guid.NewGuid(),
                                  UserId = (Guid) Cache["UserId"],
                                  Date = DateTime.Now,
                                  WarningText = warningText.Text
                              };
            AddWarning(warning);
            Response.Redirect("~/user/warnings.aspx?userId="+warning.UserId);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (this.IsSet("action") && Roles.IsUserInRole("Admin"))
                {
                    switch (Request.Params["action"])
                    {
                        case "add":
                            Guid userId;
                            if (this.IsSet("userId") && Guid.TryParse(Request.Params["userId"], out userId))
                            {
                                ReadUserStatus(userId);
                                if (IsBanned != true)
                                {
                                    multiView.SetActiveView(formView);
                                    Cache.Insert("UserId", userId);
                                }
                                else
                                {
                                    multiView.SetActiveView(viewUserBanned);
                                }
                            }
                            else
                            {
                                Response.Redirect(Request.UrlReferrer.AbsoluteUri);
                            }
                            break;
                        case "delete":
                            Guid warningId;
                            if (this.IsSet("warningId") && Guid.TryParse(Request.Params["warningId"], out warningId))
                            {
                                DeleteWarning(warningId);
                                Response.Redirect(Request.UrlReferrer.AbsoluteUri);
                            }
                            break;
                        default:
                            Response.Redirect(Request.UrlReferrer.AbsoluteUri);
                            break;
                    }
                }
                else
                {
                    Guid userId;
                    if (this.IsSet("userId") && Guid.TryParse(Request.Params["userId"], out userId))
                    {
                        ReadUserStatus(userId);
                        if (!IsBanned)
                        {
                            multiView.SetActiveView(rptWarningsView);
                            ReadWarnings(userId);
                            rptWarnings.DataSource = WarningList;
                            rptWarnings.DataBind();
                        }
                        else
                        {
                            multiView.SetActiveView(viewUserBanned);
                        }
                    }
                    else
                    {
                        Response.Redirect("~/user/warnings.aspx?userId=" + Membership.GetUser(User.Identity.Name).ProviderUserKey);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error(ex.Message);
            }
        }

        public void ReadWarnings(Guid userId)
        {
            UserId = userId;
            if(WarningsReading != null)
            {
                WarningsReading();
            }
        }
        public void AddWarning(Warning warning)
        {
            Warning = warning;
            if (WarningAdding != null)
            {
                WarningAdding();
            }
        }
        public void DeleteWarning(Guid warningId)
        {
            WarningId = warningId;
            if (WarningDeleting != null)
            {
                WarningDeleting();
            }
        }
        public void ReadUserStatus(Guid userId)
        {
            UserId = userId;
            if (UserStatusReading != null)
            {
                UserStatusReading();
            }
        }
    }
}