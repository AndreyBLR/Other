using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBPresentation.Interfaces.PresenterInterfaces.Page
{
    public interface IUsersPagePresenter
    {
        void Ban();
        void Unban();
        void AddWarning();
        void DeleteWarning();
        void ReadUser();
        void ReadUserStatus();
    }
}
