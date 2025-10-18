using MaterialDesignThemes.Wpf;
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
    public class CategoryModule : IModule
    {
        public CategoryModule(IContainerExtension container)
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
            if (registry.IsRegistered<ICategoryUnitOfWork>() == false) registry.RegisterSingleton<ICategoryUnitOfWork, CategoryUnitOfWork>();
            
            var navItemCategories = NavigationService.Create(Guid.NewGuid().ToString(), 3000, "Categories", nameof(CategoryListView), PackIconKind.InvoiceLineItems, typeof(CategoryListView));
            var navItemOverview = NavigationService.Create(Guid.NewGuid().ToString(), 10000, "Overview", nameof(CategoryListView), PackIconKind.InvoiceLineItems, typeof(CategoryListView));
            var navItemStatistics = NavigationService.Create(Guid.NewGuid().ToString(), 20000, "Statictics", nameof(CategoryListView), PackIconKind.InvoiceLineItems, typeof(CategoryListView));
            
            navItemCategories.Children.Add(navItemOverview);
            navItemCategories.Children.Add(navItemStatistics);
            NavigationService.Add(navItemCategories);

            registry.Register<CategoryListViewModel>();
            registry.Register<CategoryViewModel>();
            //registry.Register<EditCategoryDialogViewModel>();
            //registry.Register<AddCategoryDialogViewModel>();

            //registry.RegisterDialog<AddCategoryDialogView, AddCategoryDialogViewModel>(nameof(AddCategoryDialogView));
            //registry.RegisterDialog<EditCategoryDialogView, EditCategoryDialogViewModel>(nameof(EditCategoryDialogView));
            registry.RegisterForNavigation<CategoryListView>(nameof(CategoryListView));
        }
    }
}