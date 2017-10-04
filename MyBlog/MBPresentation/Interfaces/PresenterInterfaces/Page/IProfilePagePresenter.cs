using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBPresentation.Interfaces.PresenterInterfaces.Page
{
    public interface IProfilePagePresenter
    {
        void UpdateProfile();
        void ReadProfile();
        void CreateImage();
    }
}
