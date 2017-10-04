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
using MSExchangeClient.Modules.Mail.Dialogs.SendMessage.Interfaces;

namespace MSExchangeClient.Modules.Mail.Dialogs.SendMessage
{
    /// <summary>
    /// Interaction logic for ISendMessageView.xaml
    /// </summary>
    public partial class SendMessageView : Window, ISendMessageView
    {
        public SendMessageView()
        {
            InitializeComponent();
        }
    }
}
