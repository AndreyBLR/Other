using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MSExchangeClient.Infrastructure;
using MSExchangeClient.Modules.Core.SideBar.Interfaces;

namespace MSExchangeClient.Modules.Core.SideBar
{
    /// <summary>
    /// Interaction logic for SideBarView.xaml
    /// </summary>
    public partial class SideBarView : UserControl, ISideBarView
    {
        public SideBarView()
        {
            InitializeComponent();
        }

        public ISideBarViewModel ViewModel
        {
            get { return (ISideBarViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}
