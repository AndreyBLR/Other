using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBPresentation.Interfaces.PresenterInterfaces.Page
{
    public interface ICommentsPagePresenter
    {
        void ReadComment();
        void ReadComments();
        void ChangeComment();
        void DeleteComment();
    }
}
