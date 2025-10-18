using MaterialDesignThemes.Wpf;
using Northwind.Services.Interfaces;
using Northwind.ViewModels;
using Northwind.ViewModels.Charts;
using Northwind.ViewModels.Dashboards;
using Northwind.Views;
using Northwind.Widgets;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Navigation.Regions;
using System;

namespace Northwind
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
            // Create Navigation Items
            var rootDashboard = NavigationService.Create(Guid.NewGuid().ToString(), 2000, "Dashboards", nameof(DashboardView), PackIconKind.ViewDashboard, typeof(DashboardView));
            var defaultDashboard = NavigationService.Create(Guid.NewGuid().ToString(), 10000, "Standard", nameof(DashboardView), PackIconKind.ViewDashboard, typeof(DashboardView));
            var categoryDashboard = NavigationService.Create(Guid.NewGuid().ToString(), 15000, "Categories", nameof(CategoryDashboardView), PackIconKind.Group, typeof(CategoryDashboardView));
            var customerDashboard = NavigationService.Create(Guid.NewGuid().ToString(), 20000, "Customers", nameof(CustomerDashboardView), PackIconKind.UserMultiple, typeof(CustomerDashboardView));
            var employeeDashboard = NavigationService.Create(Guid.NewGuid().ToString(), 30000, "Employees", nameof(EmployeeDashboardView), PackIconKind.UserBadge, typeof(EmployeeDashboardView));
            var productDashboard = NavigationService.Create(Guid.NewGuid().ToString(), 40000, "Products", nameof(ProductDashboardView), PackIconKind.InvoiceLineItems, typeof(ProductDashboardView));
            var regionDashboard = NavigationService.Create(Guid.NewGuid().ToString(), 50000, "Regions", nameof(RegionDashboardView), PackIconKind.Map, typeof(RegionDashboardView));
            var salesDashboard = NavigationService.Create(Guid.NewGuid().ToString(), 60000, "Sales", nameof(SalesDashboardView), PackIconKind.Cart, typeof(SalesDashboardView));

            rootDashboard.Children.Add(defaultDashboard);
            rootDashboard.Children.Add(salesDashboard);
            rootDashboard.Children.Add(categoryDashboard);
            rootDashboard.Children.Add(customerDashboard);
            rootDashboard.Children.Add(employeeDashboard);
            rootDashboard.Children.Add(regionDashboard);
            rootDashboard.Children.Add(productDashboard);

            NavigationService.Add(rootDashboard);

            // Register ViewModels
            registry.Register<DashboardViewModelBase>();
            registry.Register<DashboardViewModel>();
            registry.Register<SalesDashboardViewModel>();
            registry.Register<CategoryDashboardViewModel>();
            registry.Register<CustomerDashboardViewModel>();
            registry.Register<SalesByRegionCartesianChartViewModel>();
            
            registry.Register<SalesByCategoryPieChartWidget>();

            // Register Views
            registry.RegisterForNavigation<DashboardView>(nameof(DashboardView));
            registry.RegisterForNavigation<CustomerDashboardView>(nameof(CustomerDashboardView));
            registry.RegisterForNavigation<EmployeeDashboardView>(nameof(EmployeeDashboardView));
            registry.RegisterForNavigation<ProductDashboardView>(nameof(ProductDashboardView));
            registry.RegisterForNavigation<RegionDashboardView>(nameof(RegionDashboardView));
            registry.RegisterForNavigation<SalesDashboardView>(nameof(SalesDashboardView));
            registry.RegisterForNavigation<CategoryDashboardView>(nameof(CategoryDashboardView));

        }
    }
}