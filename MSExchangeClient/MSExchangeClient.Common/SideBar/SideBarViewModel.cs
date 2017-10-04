using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MSExchangeClient.Modules.Core.SideBar.Interfaces;

namespace MSExchangeClient.Modules.Core.SideBar
{
    public class SideBarViewModel : ISideBarViewModel
    {
        public ICommand ShowSettingsCommand { get; set; }
    }
}
