using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSExchangeClient.Modules.Mail.Views.NavigationPane.Interfaces
{
    public interface INavigationPaneView
    {
        INavigationPaneViewModel ViewModel { get; set; }
    }
}
