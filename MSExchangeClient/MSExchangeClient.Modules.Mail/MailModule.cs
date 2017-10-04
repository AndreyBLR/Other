using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MSExchangeClient.Infrastructure.Enums;
using MSExchangeClient.Modules.Core.ModuleNavigationPane.Interfaces;
using MSExchangeClient.Modules.Mail.Dialogs.SendMessage;
using Microsoft.Practices.Prism.Modularity;
using MSExchangeClient.Modules.Mail.Enums;
using MSExchangeClient.Modules.Mail.Views.MailContent;
using MSExchangeClient.Modules.Mail.Views.MailContent.Interfaces;
using MSExchangeClient.Modules.Mail.Views.MailList;
using MSExchangeClient.Modules.Mail.Views.MailList.Interfaces;
using MSExchangeClient.Modules.Mail.Views.Main;
using MSExchangeClient.Modules.Mail.Views.Main.Interfaces;
using MSExchangeClient.Modules.Mail.Views.NavigationPane;
using MSExchangeClient.Modules.Mail.Views.NavigationPane.Interfaces;

namespace MSExchangeClient.Modules.Mail
{
    public class MailModule:IModule
    {

        private readonly IUnityContainer _container;
        private IRegionManager _regionManager;
        public MailModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        { 
            ConfigurationUnityContainer();
            Activate();
        }

        public void Activate()
        {
            _regionManager.Regions[RegionsNames.MainContentLocation].Add(_container.Resolve<IMailMainView>());
            _regionManager.Regions[RegionsNames.NavigationPaneLocation].Add(_container.Resolve<INavigationPanePresenter>().View);

            _regionManager.Regions[MailRegionsNames.MailListLocation].Add(_container.Resolve<IMailListPresenter>().View);
            _regionManager.Regions[MailRegionsNames.MailContentLocation].Add(_container.Resolve<IMailContentPresenter>().View);
        }

        private void ConfigurationUnityContainer()
        {
            _container.RegisterType<IMailMainView, MailMainView>();

            _container.RegisterType<IMailListView, MailListView>();
            _container.RegisterType<IMailListViewModel, MailListViewModel>();
            _container.RegisterType<IMailListPresenter, MailListPresenter>();

            _container.RegisterType<IMailContentView, MailContentView>();
            _container.RegisterType<IMailContentViewModel, MailContentViewModel>();
            _container.RegisterType<IMailContentPresenter, MailContentPresenter>();

            _container.RegisterType<INavigationPaneView, NavigationPaneView>();
            _container.RegisterType<INavigationPaneViewModel, NavigationPaneViewModel>();
            _container.RegisterType<INavigationPanePresenter, NavigationPanePresenter>();
        }
    }
}
