using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBPresentation.Interfaces.PresenterInterfaces.Page
{
    public interface IJournalPagePresenter
    {
        void ReadPosts();
        void ReadTags();
    }
}
