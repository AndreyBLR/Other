using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using MSExchangeClient.Modules.Mail.Views.NavigationPane.Interfaces;

namespace MSExchangeClient.Modules.Mail.Views.NavigationPane
{
    public class NavigationPanePresenter : INavigationPanePresenter
    {
        private readonly IUnityContainer _container;

        public NavigationPanePresenter(IUnityContainer container)
        {
            _container = container;
            View = _container.Resolve<INavigationPaneView>();
            View.ViewModel = _container.Resolve<INavigationPaneViewModel>();
        }

        public INavigationPaneView View { get; private set; }
    }
}
