using System.Windows.Controls;
using MSExchangeClient.Modules.Mail.Views.Main.Interfaces;

namespace MSExchangeClient.Modules.Mail.Views.Main
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MailMainView : UserControl, IMailMainView
    {
        public MailMainView()
        {
            InitializeComponent();
        }
    }
}
