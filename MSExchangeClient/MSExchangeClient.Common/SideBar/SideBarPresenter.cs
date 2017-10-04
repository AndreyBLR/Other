using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using MSExchangeClient.Modules.Core.Settings.Interfaces;
using MSExchangeClient.Modules.Core.SideBar.Interfaces;
using Microsoft.Practices.Unity;

namespace MSExchangeClient.Modules.Core.SideBar
{
    public class SideBarPresenter : ISideBarPresenter
    {
        private readonly IUnityContainer _container;

        public SideBarPresenter(IUnityContainer container)
        {
            _container = container;
            View = _container.Resolve<ISideBarView>();
            View.ViewModel = _container.Resolve<ISideBarViewModel>();

            View.ViewModel.ShowSettingsCommand = new DelegateCommand(OnShowSettingsExecute, OnShowSettingsCanExecute);
        }

        public ISideBarView View { get; private set; }

        #region Commands handlers

        private void OnShowSettingsExecute()
        {
            var settingsPresenter = _container.Resolve<ISettingsPresenter>();
            ((Window)settingsPresenter.View).ShowDialog();
        }
        private bool OnShowSettingsCanExecute()
        {
            return true;
        }

        #endregion
    }
}
