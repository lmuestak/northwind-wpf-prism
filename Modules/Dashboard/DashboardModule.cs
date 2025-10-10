using MaterialDesignThemes.Wpf;
using Northwind.DataAccess;
using Northwind.Modules.ViewModels;
using Northwind.Modules.Views;
using Northwind.Services.Interfaces;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Navigation.Regions;
using System;

namespace Northwind.Modules
{
    public class DashboardModule : IModule
    {

        public DashboardModule(IContainerExtension container)
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
            var rootDashboard = NavigationService.Create(Guid.NewGuid().ToString(), 2000, "Dashboards", nameof(DashboardView), PackIconKind.ViewDashboard, typeof(DashboardView));
            var defaultDashboard = NavigationService.Create(Guid.NewGuid().ToString(), 10000, "Standard", nameof(DashboardView), PackIconKind.ViewDashboard, typeof(DashboardView));
            var customerDashboard = NavigationService.Create(Guid.NewGuid().ToString(), 20000, "Customers", nameof(CustomerDashboardView), PackIconKind.UserMultiple, typeof(CustomerDashboardView));
            var employeeDashboard = NavigationService.Create(Guid.NewGuid().ToString(), 30000, "Employees", nameof(EmployeeDashboardView), PackIconKind.UserBadge, typeof(EmployeeDashboardView));
            var productDashboard = NavigationService.Create(Guid.NewGuid().ToString(), 40000, "Products", nameof(ProductDashboardView), PackIconKind.InvoiceLineItems, typeof(ProductDashboardView));
            var regionDashboard = NavigationService.Create(Guid.NewGuid().ToString(), 50000, "Regions", nameof(RegionDashboardView), PackIconKind.Map, typeof(RegionDashboardView));
            var salesDashboard = NavigationService.Create(Guid.NewGuid().ToString(), 60000, "Sales", nameof(SalesDashboardView), PackIconKind.Cart, typeof(SalesDashboardView));

            rootDashboard.Children.Add(defaultDashboard);
            rootDashboard.Children.Add(salesDashboard);
            rootDashboard.Children.Add(customerDashboard);
            rootDashboard.Children.Add(employeeDashboard);
            rootDashboard.Children.Add(regionDashboard);
            rootDashboard.Children.Add(productDashboard);

            NavigationService.Add(rootDashboard);

            registry.RegisterSingleton<IDashboardDataManager, DashboardDataManager>();  
            registry.Register<DashboardViewModel>();
            registry.RegisterForNavigation<DashboardView>(nameof(DashboardView));
            registry.RegisterForNavigation<CustomerDashboardView>(nameof(CustomerDashboardView));
            registry.RegisterForNavigation<EmployeeDashboardView>(nameof(EmployeeDashboardView));
            registry.RegisterForNavigation<ProductDashboardView>(nameof(ProductDashboardView));
            registry.RegisterForNavigation<RegionDashboardView>(nameof(RegionDashboardView));
            registry.RegisterForNavigation<SalesDashboardView>(nameof(SalesDashboardView));
        }
    }
}