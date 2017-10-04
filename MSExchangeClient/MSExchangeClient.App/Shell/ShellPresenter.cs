using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace MSExchangeClient.App.Shell
{
    public class ShellPresenter
    {
        #region Fields

        private readonly IUnityContainer _container;

        #endregion

        #region Properties

        public ShellView View { get; set; }

        #endregion

        #region Methods

        #endregion

        public ShellPresenter(IUnityContainer container)
        {
            _container = container;
            View = new ShellView();
        }
    }
}
