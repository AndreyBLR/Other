using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using MSExchangeClient.App.Shell;
using MSExchangeClient.Infrastructure.Enums;
using MSExchangeClient.Modules.Core;
using MSExchangeClient.Modules.Core.SideBar;
using MSExchangeClient.Modules.Mail;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.ServiceLocation;

namespace MSExchangeClient.App
{
    internal class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return new ShellPresenter(Container).View;
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (ShellView) this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            this.ModuleCatalog.AddModule(DescribeModule(typeof(MailModule)));
            this.ModuleCatalog.AddModule(DescribeModule(typeof(CoreModule)));
        }
        private ModuleInfo DescribeModule(Type type)
        {
            string moduleName = type.Name;
            var dependsOn = new List<string>();
            var onDemand = false;
            var moduleAttribute =
                CustomAttributeData.GetCustomAttributes(type).FirstOrDefault(
                    cad => cad.Constructor.DeclaringType.FullName == typeof(ModuleAttribute).FullName);

            if (moduleAttribute != null)
            {
                foreach (CustomAttributeNamedArgument argument in moduleAttribute.NamedArguments)
                {
                    string argumentName = argument.MemberInfo.Name;
                    switch (argumentName)
                    {
                        case "ModuleName":
                            moduleName = (string)argument.TypedValue.Value;
                            break;

                        case "OnDemand":
                            onDemand = (bool)argument.TypedValue.Value;
                            break;

                        case "StartupLoaded":
                            onDemand = !((bool)argument.TypedValue.Value);
                            break;
                    }
                }
            }

            var moduleDependencyAttributes =
                CustomAttributeData.GetCustomAttributes(type).Where(
                    cad => cad.Constructor.DeclaringType.FullName == typeof(ModuleDependencyAttribute).FullName);

            foreach (CustomAttributeData cad in moduleDependencyAttributes)
            {
                dependsOn.Add((string)cad.ConstructorArguments[0].Value);
            }

            var moduleInfo = new ModuleInfo(moduleName, type.AssemblyQualifiedName)
            {
                InitializationMode =
                    onDemand
                        ? InitializationMode.OnDemand
                        : InitializationMode.WhenAvailable,
                Ref = type.Assembly.CodeBase,
            };
            moduleInfo.DependsOn.AddRange(dependsOn);
            return moduleInfo;
        }
    }
}
