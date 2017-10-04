using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.Interfaces.PresenterInterfaces.Page;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.ServiceReference;

namespace MBPresentation.Presenters.Page
{
    public class UsersPagePresenter: IUsersPagePresenter
    {
        private IUsersPage _page;

        public UsersPagePresenter(IUsersPage page)
        {
            _page = page;
            _page.UserBanning += Ban;
            _page.UserUnbannig += Unban;
            _page.WarningAdding += AddWarning;
            _page.WarningDeleting += DeleteWarning;
            _page.UserReading += ReadUser;
            _page.UserStatusReading += ReadUserStatus;
        }


        public void Ban()
        {
            var svc = new ModelServiceClient();
            svc.BanUser(_page.UserId);
        }
        public void Unban()
        {
            var svc = new ModelServiceClient();
            svc.UnBanUser(_page.UserId);
        }
        public void AddWarning()
        {
            var svc = new ModelServiceClient();
            svc.AddWarningUser(_page.UserId);
        }
        public void DeleteWarning()
        {
            var svc = new ModelServiceClient();
            svc.DeleteWarningUser(_page.UserId);
        }

        public void ReadUser()
        {
            var svc = new ModelServiceClient();
            _page.UserProfile = svc.ReadProfileById(_page.UserId);
        }

        public void ReadUserStatus()
        {
            var svc = new ModelServiceClient();
            _page.IsBanned = Convert.ToBoolean(svc.ReadProfileById(_page.UserId).IsBanned);
        }
    }
}
