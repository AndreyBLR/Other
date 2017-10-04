using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using MSExchangeClient.Modules.Mail.Views.MailList.Interfaces;

namespace MSExchangeClient.Modules.Mail.Views.MailList
{
    public class MailListPresenter:IMailListPresenter
    {
        private readonly IUnityContainer _container;

        public MailListPresenter(IUnityContainer container)
        {
            _container = container;
            View = _container.Resolve<IMailListView>();
            View.ViewModel = _container.Resolve<IMailListViewModel>();
        }

        public IMailListView View { get; private set; }
    }
}
