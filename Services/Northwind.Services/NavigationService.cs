using MaterialDesignThemes.Wpf;
using Northwind.Mvvm;
using Northwind.Services.Interfaces;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Northwind.Services
{
    public class NavigationService :ViewModelBase, INavigationService
    {

        private ObservableCollection<INavigationItemViewModel> _items;

        public NavigationService(IContainerExtension container) : base(container)
        {
        }
        public ObservableCollection<INavigationItemViewModel> Items
        {
            get
            {
                if (_items == null)
                {
                    var items = Container.Resolve<INavigationItemViewModel[]>();
                    _items ??= new ObservableCollection<INavigationItemViewModel>(items);
                }
                if(_items != null)
                {
                    var items = _items.OrderBy(c => c.Order).ThenBy(c => c.Name).ToList();
                    foreach (var item in items)
                    {
                        SortChildren(item);
                    }
                    _items = new ObservableCollection<INavigationItemViewModel>(items);
                }
                return _items;
            }
            private set => _items = value;
        }

        public void Add(INavigationItemViewModel child)
        {
            Items.Add(child);
            RaisePropertyChanged(nameof(Items));
        }

        public void AddChild(INavigationItemViewModel parent, INavigationItemViewModel child)
        {
            if (parent == null || child == null) return;
            child.Parent = parent;
            parent.Children.Add(child);
            RaisePropertyChanged(nameof(Items));
        }

        public INavigationItemViewModel Create(string id, int order, string name, string target, PackIconKind icon, Type viewType)
        {
            var item = Container.Resolve<INavigationItemViewModel>();
            item.Id = id;
            item.Order = order;
            item.Name = name;
            item.Target = target;
            item.Icon = icon;
            item.Tag = viewType;
            return item;
        }

        public void Delete(INavigationItemViewModel item)
        {
            if (item?.Parent == null) return;
            item.Parent.Children.Remove(item);
            item.Parent = null;
            RaisePropertyChanged(nameof(Items));
        }

        public void Delete(INavigationItemViewModel parent, INavigationItemViewModel itemToDelete)
        {
            if (parent == null || itemToDelete == null) return;
            if (itemToDelete.Parent != parent) return;
            parent.Children.Remove(itemToDelete);
            itemToDelete.Parent = null;
            RaisePropertyChanged(nameof(Items));
        }

        public void Move(INavigationItemViewModel item, INavigationItemViewModel newParent)
        {
            if (item?.Parent == null || newParent == null) return;

            item.Parent.Children.Remove(item);
            newParent.Children.Add(item);
            item.Parent = newParent;
            RaisePropertyChanged(nameof(Items));
        }

        public void Replace(INavigationItemViewModel oldItem, INavigationItemViewModel newItem, bool keepChildren = true)
        {
            if (oldItem?.Parent == null || newItem == null) return;

            var parent = oldItem.Parent;
            int index = parent.Children.IndexOf(oldItem);

            if (keepChildren)
            {
                foreach (var child in oldItem.Children)
                {
                    newItem.Children.Add(child);
                    child.Parent = newItem;
                }
            }

            parent.Children[index] = newItem;
            newItem.Parent = parent;
            RaisePropertyChanged(nameof(Items));
        }

        public void SortChildren(INavigationItemViewModel parent)
        {
            if (parent == null) return;

            var sorted = parent.Children.OrderBy(c => c.Order).ThenBy(c => c.Name).ToList();
            parent.Children.Clear();
            foreach (var item in sorted)
            {
                parent.Children.Add(item);
            }
        }
    }
}
