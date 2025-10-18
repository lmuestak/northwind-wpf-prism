using MaterialDesignThemes.Wpf;
using Northwind.Mvvm;
using Northwind.Services.Interfaces;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;

namespace Northwind.Services
{

    public class NavigationItemViewModel : ViewModelBase, INavigationItemViewModel
    {

        private string _id = Guid.NewGuid().ToString();
        private int _order;
        private string _name;
        private string _tooltip;
        private object _tag;
        private PackIconKind _icon;
        private string _target;
        private bool _isActive;
        private bool _isExpanded;
        private INavigationItemViewModel _parent;
        private ObservableCollection<INavigationItemViewModel> _children;

        //public NavigationItemViewModel(IContainerExtension container) : base(container)
        //{
        //}

        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public int Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Tooltip
        {
            get => _tooltip;
            set => SetProperty(ref _tooltip, value);
        }

        public object Tag
        {
            get => _tag;
            set => SetProperty(ref _tag, value);
        }

        public PackIconKind Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        public string Target
        {
            get => _target;
            set => SetProperty(ref _target, value);
        }

        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }

        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetProperty(ref _isExpanded, value);
        }

        public bool IsLeaf => Children == null || Children.Count == 0;
        public INavigationItemViewModel Parent
        {
            get => _parent;
            set => SetProperty(ref _parent, value);
        }

        public bool HasChildren => Children != null && Children.Count > 0;
        public bool HasNoChildren => Children == null || Children.Count == 0;
        
        public ObservableCollection<INavigationItemViewModel> Children => _children ??= new ObservableCollection<INavigationItemViewModel>(Container.Resolve<INavigationItemViewModel[]>());
    }
}
