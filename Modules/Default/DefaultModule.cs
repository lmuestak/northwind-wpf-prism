using MaterialDesignThemes.Wpf;
using Northwind.Services.Interfaces;
using Northwind.ViewModels;
using Northwind.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Navigation.Regions;
using System;

namespace Northwind
{
    public class DefaultModule : IModule
    {
        public DefaultModule(IContainerExtension container)
        {
            Container = container;
        }

        public IContainerExtension Container { get; private set; }
        public IRegionManager RegionManager => Container.Resolve<IRegionManager>();
        public INavigationService NavigationService => Container.Resolve<INavigationService>();
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //RegionManager.RequestNavigate(RegionNames.ContentRegion, nameof(DefaultView));
        }

        public void RegisterTypes(IContainerRegistry registry)
        {
            var navItem = NavigationService.Create(Guid.NewGuid().ToString(), 1000, "Home", nameof(DefaultView), PackIconKind.Home, typeof(DefaultView));
            var navItem2 = NavigationService.Create(Guid.NewGuid().ToString(), 450000, "Settings", nameof(SettingsView), PackIconKind.Settings, typeof(SettingsView));
            var navItem3 = NavigationService.Create(Guid.NewGuid().ToString(), 500000, "Home", nameof(HelpView), PackIconKind.HelpBoxMultiple, typeof(HelpView));
            
            NavigationService.Add(navItem);
            NavigationService.Add(navItem2);
            NavigationService.Add(navItem3);

            registry.Register<DefaultViewModel>();
            registry.Register<HelpViewModel>();
            registry.Register<SettingsViewModel>();
            registry.Register<DefaultView>();
            registry.Register<SettingsView>();
            registry.Register<HelpView>();

            registry.RegisterForNavigation<DefaultView>(nameof(DefaultView));
            registry.RegisterForNavigation<SettingsView>(nameof(SettingsView));
            registry.RegisterForNavigation<HelpView>(nameof(HelpView));
        }
    }
}