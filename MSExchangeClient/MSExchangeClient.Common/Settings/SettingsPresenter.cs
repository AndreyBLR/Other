using System;
using Microsoft.Practices.Unity;
using MSExchangeClient.Modules.Core.Settings.Interfaces;

namespace MSExchangeClient.Modules.Core.Settings
{
    public class SettingsPresenter:ISettingsPresenter
    {

        private readonly IUnityContainer _container;

        public SettingsPresenter(IUnityContainer container)
        {
            _container = container;
            View = _container.Resolve<ISettingsView>();
        }

        public ISettingsView View { get; private set; }
    }
}
