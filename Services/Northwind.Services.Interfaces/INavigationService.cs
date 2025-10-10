using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;

namespace Northwind.Services.Interfaces
{
    public interface INavigationService
    {
        ObservableCollection<INavigationItemViewModel> Items { get; }
        INavigationItemViewModel Create(string id, int order, string name, string target, PackIconKind icon, Type viewType);
        void Add(INavigationItemViewModel child);
        void AddChild(INavigationItemViewModel parent, INavigationItemViewModel child);
        void Replace(INavigationItemViewModel oldItem, INavigationItemViewModel newItem, bool keepChildren = true);
        void Move(INavigationItemViewModel item, INavigationItemViewModel newParent);
        void SortChildren(INavigationItemViewModel parent);
        void Delete(INavigationItemViewModel item);
        void Delete(INavigationItemViewModel parent, INavigationItemViewModel itemToDelete);
    }
}
