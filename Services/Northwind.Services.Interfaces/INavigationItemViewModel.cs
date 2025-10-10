using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;

namespace Northwind.Services.Interfaces
{
    public interface INavigationItemViewModel
    {
        string Id { get; set; }
        int Order { get; set; }
        string Name { get; set; }
        string Tooltip { get; set; }
        object Tag { get; set; }
        PackIconKind Icon { get; set; }
        string Target { get; set; }
        bool IsActive { get; set; }
        bool IsExpanded { get; set; }
        bool IsLeaf { get; }
        bool HasChildren { get; }
        bool HasNoChildren { get; }
        INavigationItemViewModel Parent { get; set; }
        ObservableCollection<INavigationItemViewModel> Children { get; }
    }

}
