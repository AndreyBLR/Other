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
using MSExchangeClient.Modules.Core.ModuleNavigationPane.Interfaces;
using MSExchangeClient.Modules.Core.ModuleNavigationPane.Interfaces;

namespace MSExchangeClient.Modules.Core.ModuleNavigationPane
{
    /// <summary>
    /// Interaction logic for ModuleNavigationPaneView.xaml
    /// </summary>
    public partial class ModuleNavigationPaneView : UserControl, IModuleNavigationPaneView
    {
        public ModuleNavigationPaneView()
        {
            InitializeComponent();
        }

        public IModuleNavigationPaneViewModel ViewModel { get; set; }
    }
}
