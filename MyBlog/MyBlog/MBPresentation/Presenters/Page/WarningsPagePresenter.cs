using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.Interfaces.PresenterInterfaces.Page;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.ServiceReference;

namespace MBPresentation.Presenters.Page
{
    public class WarningsPagePresenter:IWarningsPagePresenter
    {
        private IWarningsPage _page;

        public WarningsPagePresenter(IWarningsPage page)
        {
            _page = page;
            _page.WarningAdding += AddWarning;
            _page.WarningDeleting += DeleteWarning;
            _page.WarningsReading += ReadWarnings;
            _page.UserStatusReading += ReadUserStatus;
        }

        public void ReadWarnings()
        {
            var svc = new ModelServiceClient();
            _page.WarningList = svc.ReadWarnings(_page.UserId).ToList();
        }
        public void AddWarning()
        {
            var svc = new ModelServiceClient();
            svc.CreateWarning(_page.Warning);
        }
        public void DeleteWarning()
        {
            var svc = new ModelServiceClient();
            svc.DeleteWarning(_page.WarningId);
        }
        public void ReadUserStatus()
        {
            var svc = new ModelServiceClient();
            _page.IsBanned = Convert.ToBoolean(svc.ReadProfileById(_page.UserId).IsBanned);
        }
    }
}
