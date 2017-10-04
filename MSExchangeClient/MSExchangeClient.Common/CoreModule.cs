using MSExchangeClient.Infrastructure.Enums;
using MSExchangeClient.Modules.Core.ModuleNavigationPane;
using MSExchangeClient.Modules.Core.ModuleNavigationPane.Interfaces;
using MSExchangeClient.Modules.Core.Settings;
using MSExchangeClient.Modules.Core.Settings;
using MSExchangeClient.Modules.Core.Settings.Interfaces;
using MSExchangeClient.Modules.Core.SideBar;
using MSExchangeClient.Modules.Core.SideBar.Interfaces;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace MSExchangeClient.Modules.Core
{
    public class CoreModule:IModule
    {
        private IUnityContainer _container;
        private IRegionManager _regionManager;
        public CoreModule(IUnityContainer container,  IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            ConfigureUnityContainer();
            _regionManager.Regions[RegionsNames.SideBarLocation].Add(_container.Resolve<ISideBarPresenter>().View);
            _regionManager.Regions[RegionsNames.ModuleNavigationLocation].Add(_container.Resolve<IModuleNavigationPanePresenter>().View);

        }

        private void ConfigureUnityContainer()
        {
            _container.RegisterType<ISettingsView, SettingsView>();
            _container.RegisterType<ISettingsViewModel, SettingsViewModel>();
            _container.RegisterType<ISettingsPresenter, SettingsPresenter>();

            _container.RegisterType<ISideBarView, SideBarView>();
            _container.RegisterType<ISideBarViewModel, SideBarViewModel>();
            _container.RegisterType<ISideBarPresenter, SideBarPresenter>();

            _container.RegisterType<IModuleNavigationPaneView, ModuleNavigationPaneView>();
            _container.RegisterType<IModuleNavigationPaneViewModel, ModuleNavigationPaneViewModel>();
            _container.RegisterType<IModuleNavigationPanePresenter, ModuleNavigationPanePresenter>();
        }
    }
}
