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
    public class OrderModule : IModule
    {
        public OrderModule(IContainerExtension container)
        {
            Container = container;
        }
        public IContainerExtension Container { get; private set; }
        public IRegionManager RegionManager => Container.Resolve<IRegionManager>();
        public INavigationService NavigationService => Container.Resolve<INavigationService>();

        public void OnInitialized(IContainerProvider provider)
        {
        }

        public void RegisterTypes(IContainerRegistry registry)
        {
            var navItem = NavigationService.Create(Guid.NewGuid().ToString(), 5000, "Orders", nameof(OrderListView), PackIconKind.AccountGroup, typeof(OrderListView));
            NavigationService.Add(navItem);
            registry.Register<OrderListViewModel>();
            registry.RegisterForNavigation<OrderListView>(nameof(OrderListView));
        }
    }
}