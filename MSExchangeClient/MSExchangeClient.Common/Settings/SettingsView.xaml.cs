using System.Windows;
using MSExchangeClient.Modules.Core.Settings.Interfaces;

namespace MSExchangeClient.Modules.Core.Settings
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window, ISettingsView
    {
        public SettingsView()
        {
            InitializeComponent();
        }
    }
}
