using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSExchangeClient.Modules.Core.SideBar.Interfaces
{
    public interface ISideBarView
    {
        ISideBarViewModel ViewModel { get; set; }
    }
}
