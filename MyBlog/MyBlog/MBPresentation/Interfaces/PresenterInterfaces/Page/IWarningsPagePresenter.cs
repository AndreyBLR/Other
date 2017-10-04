using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBPresentation.Interfaces.PresenterInterfaces.Page
{
    public interface IWarningsPagePresenter
    {
        void ReadWarnings();
        void AddWarning();
        void DeleteWarning();
        void ReadUserStatus();
    }
}
