using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBPresentation.Interfaces.PresenterInterfaces.Page
{
    public interface IArticlePagePresenter
    {
        void ReadPost();
        void CreateComment();
        void ReadUserStatus();
    }
}
