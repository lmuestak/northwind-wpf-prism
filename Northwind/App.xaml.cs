using Northwind.DataAccess;
using Northwind.Dialogs;
using Northwind.Mvvm;
using Northwind.Services;
using Northwind.Services.Interfaces;
using Northwind.ViewModels;
using Northwind.Windows;
using Prism.Dialogs;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

namespace Northwind
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {

        private SplashWindow _splashWindow;
        private ShellWindow _shellWindow;

        protected override void OnStartup(StartupEventArgs e)
        {
            _splashWindow = new SplashWindow(){ DataContext = new SplashWindowViewModel() };
            _splashWindow.Show();
            _splashWindow.Activate();
            base.OnStartup(e);
        }

        protected override Window CreateShell()
        {
            _shellWindow = Container.Resolve<ShellWindow>();
            _shellWindow.Hide();
            return _shellWindow;
        }

        protected override void RegisterTypes(IContainerRegistry container)
        {
            container.Register<ViewModelBase>();
            container.Register<SplashWindowViewModel>();
            container.RegisterSingleton<IConfigurationService, ConfigurationService>();

            RestoreDbIfNotExists();
            RegisterDialogs(container);
            
            container.Register<IShellWindowViewModel, ShellWindowViewModel>();
            container.Register<INavigationItemViewModel, NavigationItemViewModel>();
            container.RegisterSingleton<INavigationService, NavigationService>();
            container.RegisterSingleton<IMessageService, MessageService>();

            RegisterDataAccessServices(container);

            container.RegisterSingleton<ShellWindowViewModel>();
            container.RegisterSingleton<ShellWindow>();
        }

        private void RegisterDialogs(IContainerRegistry container)
        {
            container.RegisterDialogWindow<Windows.DialogWindow>();
            container.RegisterSingleton<Services.Interfaces.IDialogService, Services.DialogService>();
            container.RegisterDialog<LoginDialogView, LoginDialogViewModel>(nameof(LoginDialogView));
        }

        private void RegisterDataAccessServices(IContainerRegistry container)
        {
            container.RegisterSingleton<IDashboardDataManager, DashboardDataManager>();
            container.RegisterSingleton<ICategoryUnitOfWork, CategoryUnitOfWork>();
            container.RegisterSingleton<ICustomerUnitOfWork, CustomerUnitOfWork>();
            container.RegisterSingleton<IEmployeeTerritoryUnitOfWork, EmployeeTerritoryUnitOfWork>();
            container.RegisterSingleton<IEmployeeUnitOfWork, EmployeeUnitOfWork>();
            container.RegisterSingleton<IOrderDetailUnitOfWork, OrderDetailUnitOfWork>();
            container.RegisterSingleton<IOrderUnitOfWork, OrderUnitOfWork>();
            container.RegisterSingleton<IProductUnitOfWork, ProductUnitOfWork>();
            container.RegisterSingleton<IRegionUnitOfWork, RegionUnitOfWork>();
            container.RegisterSingleton<IShipperUnitOfWork, ShipperUnitOfWork>();
            container.RegisterSingleton<ISupplierUnitOfWork, SupplierUnitOfWork>();
            container.RegisterSingleton<ITerritoryUnitOfWork, TerritoryUnitOfWork>();
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<DefaultModule>();
            moduleCatalog.AddModule<DashboardModule>();
            moduleCatalog.AddModule<CategoryModule>();
            moduleCatalog.AddModule<ProductModule>();
            moduleCatalog.AddModule<OrderModule>();
            moduleCatalog.AddModule<CustomerModule>();
            moduleCatalog.AddModule<EmployeeModule>();
        }

        public IModuleCatalog ModuleCatalog => Container.Resolve<IModuleCatalog>();
        public IModuleManager ModuleManager => Container.Resolve<IModuleManager>();

        protected override void InitializeModules()
        {
            var maximum = ModuleCatalog.Modules.Count();
            var value = 1;
            _splashWindow.SetMaximum(maximum);
            _splashWindow.SetStatus($"Initializing the modules...", value);
            foreach (var moduleInfo in ModuleCatalog.Modules)
            {
                _splashWindow.SetStatus($"Loading {moduleInfo.ModuleName}...", value);
                DoEvents();
                ModuleManager.LoadModule(moduleInfo.ModuleName);
                value++;
            }
            _splashWindow.SetStatus($"Modules are initialized...", value);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _splashWindow.Close();
            _shellWindow.Hide();
            var dialogService = Container.Resolve<Services.Interfaces.IDialogService>();
            var callback = new DialogCallback()
            .OnClose(result =>
            {
                if (result.Result != ButtonResult.OK)
                {
                    Current.Shutdown();
                    Console.WriteLine("Confirm cancelled");
                    return;
                }
                _shellWindow.Show();
                Console.WriteLine($"Confirm closed: {result.Result}");
            })
            .OnError(ex =>
            {
                Current.Shutdown();
                Console.WriteLine($"Confirm error: {ex.GetType().Name}: {ex.Message}");
            });
            dialogService.ShowDialog(nameof(LoginDialogView), null, callback);
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewName = viewType.FullName.Replace(".Views.", ".ViewModels."); // Example: change namespace
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = $"{viewName}Model, {viewAssemblyName}";
                var type = Type.GetType(viewModelName);
                if(type == null)
                {
                    viewName = viewType.FullName.Replace(".Windows.", ".ViewModels."); // Example: change namespace
                    viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                    viewModelName = $"{viewName}ViewModel, {viewAssemblyName}";
                    type = Type.GetType(viewModelName);
                }
                if (type == null)
                {
                    viewName = viewType.FullName.Replace(".Widgets.", ".ViewModels.Widgets."); // Example: change namespace
                    viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                    viewModelName = $"{viewName}ViewModel, {viewAssemblyName}";
                    type = Type.GetType(viewModelName);
                }
                return type;
            });
        }

        private static void DoEvents()
        {
            Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { /*Thread.Sleep(2000);*/ }));
        }

        private static void RestoreDbIfNotExists()
        {
            try
            {
                var dbName = "Northwind.sqlite";
#if DEBUG
                dbName = "Northwind.Debug.sqlite";
#endif
                var target = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{dbName}");
                var uri = new Uri($"pack://application:,,,/Northwind;component/Resources/Database/{dbName}");
                var info = GetResourceStream(uri);
                if (info != null && !System.IO.File.Exists(target))
                {
                    using var source = info.Stream;
                    using var dest = System.IO.File.Create(target);
                    source.CopyTo(dest);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            var configurationService = Container.Resolve<IConfigurationService>();
            // Save the current theme and language settings before exiting
            configurationService.Save();
            base.OnExit(e);
        }
    }
}
