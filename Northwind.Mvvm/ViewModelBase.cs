using Northwind.Services.Interfaces;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Navigation.Regions;
using System;
using Unity;

namespace Northwind.Mvvm
{
    public abstract class ViewModelBase : BindableBase, INavigationAware, IConfirmNavigationRequest, IDestructible
    {

#if DEBUG
                private bool _isInDebugMode = true;
#else
                private bool _isInDebugMode = false;
#endif
        #region services

        [Dependency]
        public IContainerExtension Container { get ; set; }
        public IRegionManager RegionManager => Container.Resolve<IRegionManager>();
        public IModuleManager ModuleManager => Container.Resolve<IModuleManager>();
        public IModuleCatalog ModuleCatalog => Container.Resolve<IModuleCatalog>();
        public IConfigurationService ConfigurationService => Container.Resolve<IConfigurationService>();
        public IMessageService MessageService => Container.Resolve<IMessageService>();
        public INavigationService NavigationService => Container.Resolve<INavigationService>();
        public IShellWindowViewModel ShellViewModel => Container.Resolve<IShellWindowViewModel>();
         
        #endregion

        #region loading indicator

        private bool _isLoading = false;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public bool IsInDebugMode
        {
            get => _isInDebugMode;
            set => SetProperty(ref _isInDebugMode, value);
        }

#endregion

            #region navigation

        public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            continuationCallback(true);
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        #endregion

        #region destruction

        public virtual void Destroy()
        {

        }

        #endregion

    }

}
