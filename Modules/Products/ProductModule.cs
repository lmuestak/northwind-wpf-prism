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
    public class ProductModule : IModule
    {
        public ProductModule(IContainerExtension container)
        {
            Container = container;
        }
        public IContainerExtension Container { get; private set; }
        public IRegionManager RegionManager => Container.Resolve<IRegionManager>();
        public INavigationService NavigationService => Container.Resolve<INavigationService>();

        public void OnInitialized(IContainerProvider provider)
        {
            //RegionManager.RequestNavigate(RegionNames.ContentRegion, nameof(EmployeeListView));
        }

        public void RegisterTypes(IContainerRegistry registry)
        {
            var navItem = NavigationService.Create(Guid.NewGuid().ToString(), 4000, "Products", nameof(ProductListView), PackIconKind.AccountGroup, typeof(ProductListView));
            NavigationService.Add(navItem);
            registry.Register<ProductListViewModel>();
            registry.RegisterForNavigation<ProductListView>(nameof(ProductListView));
        }
    }
}