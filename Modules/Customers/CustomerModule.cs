using MaterialDesignThemes.Wpf;
using Northwind.Modules.ViewModels;
using Northwind.Modules.Views;
using Northwind.Services.Interfaces;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Navigation.Regions;
using System;

namespace Northwind.Modules
{
    public class CustomerModule : IModule
    {
        public CustomerModule(IContainerExtension container)
        {
            Container = container;
        }
        public IContainerExtension Container { get; private set; }
        public IRegionManager RegionManager => Container.Resolve<IRegionManager>();
        public INavigationService NavigationService => Container.Resolve<INavigationService>();

        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry registry)
        {
            var navItem = NavigationService.Create(Guid.NewGuid().ToString(), 6000, "Customers", nameof(CustomerListView), PackIconKind.CustomerService, typeof(CustomerListView));
            NavigationService.Add(navItem);
            registry.Register<CustomerListViewModel>();
            registry.RegisterForNavigation<CustomerListView>(nameof(CustomerListView));
        }
    }
}