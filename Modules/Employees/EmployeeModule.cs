using MaterialDesignThemes.Wpf;
using Northwind.Data;
using Northwind.DataAccess;
using Northwind.Services.Interfaces;
using Northwind.ViewModels;
using Northwind.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Navigation.Regions;
using System;

namespace Northwind
{

    public class EmployeeModule : IModule
    {
        public EmployeeModule(IContainerExtension container)
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
            var navItem = NavigationService.Create(Guid.NewGuid().ToString(), 8000, "Employees", nameof(EmployeeListView), PackIconKind.AccountGroup, typeof(EmployeeListView));
            NavigationService.Add(navItem);
            registry.Register<Employee>();
            registry.Register<EmployeeListViewModel>();
            //registry.Register<IEmployeeViewModel, EmployeeViewModel>();
            if (registry.IsRegistered<IEmployeeUnitOfWork>() == false)
            {
                registry.RegisterSingleton<IEmployeeUnitOfWork, EmployeeUnitOfWork>();
            }
            registry.RegisterForNavigation<EmployeeListView>(nameof(EmployeeListView));
        }
    }
}