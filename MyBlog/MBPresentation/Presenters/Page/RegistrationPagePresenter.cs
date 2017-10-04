using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.Interfaces.PresenterInterfaces.Page;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.ServiceReference;

namespace MBPresentation.Presenters.Page
{
    public class RegistrationPagePresenter: IRegistrationPagePresenter
    {
        private IRegistrationPage _page;

        public RegistrationPagePresenter(IRegistrationPage page)
        {
            _page = page;
            _page.ProfileCreating += CreateProfile;
        }
        public void CreateProfile()
        {
            var svc = new ModelServiceClient();
            svc.CreateProfile(_page.UserProfile);
        }
    }
}
