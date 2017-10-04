using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.Interfaces.PresenterInterfaces.Page;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.ServiceReference;

namespace MBPresentation.Presenters.Page
{
    public class ProfilePagePresenter: IProfilePagePresenter
    {
        private IProfilePage _page;

        public ProfilePagePresenter(IProfilePage page)
        {
            _page = page;
            _page.ProfileReading += ReadProfile;
            _page.ProfileUpdating += UpdateProfile;
            _page.ImageCreating += CreateImage;
        }

        public void UpdateProfile()
        {
            var svc = new ModelServiceClient();
            svc.UpdateProfile(_page.UserProfile);
        }

        public void ReadProfile()
        {
            var svc = new ModelServiceClient();
            _page.UserProfile = svc.ReadProfileById(_page.UserId);
        }

        public void CreateImage()
        {
            var svc = new ModelServiceClient();
            svc.CreateImage(_page.Avatar);
        }
    }
}
