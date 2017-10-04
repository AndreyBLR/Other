using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using MSExchangeClient.Modules.Core.ModuleNavigationPane.Interfaces;

namespace MSExchangeClient.Modules.Core.ModuleNavigationPane
{
    public class ModuleNavigationPanePresenter : IModuleNavigationPanePresenter
    {

        private readonly IUnityContainer _container;

        public ModuleNavigationPanePresenter(IUnityContainer container)
        {
            _container = container;
            View = _container.Resolve<IModuleNavigationPaneView>();
            View.ViewModel = _container.Resolve<IModuleNavigationPaneViewModel>();
        }

        public IModuleNavigationPaneView View { get; private set; }
    }
}
