using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using MSExchangeClient.Modules.Mail.Views.MailContent.Interfaces;

namespace MSExchangeClient.Modules.Mail.Views.MailContent
{
    public class MailContentPresenter:IMailContentPresenter
    {
        private readonly IUnityContainer _container;

        public MailContentPresenter(IUnityContainer container)
        {
            _container = container;
            View = _container.Resolve<IMailContentView>();
            View.ViewModel = _container.Resolve<IMailContentViewModel>();
        }

        public IMailContentView View { get; private set; }
    }
}
