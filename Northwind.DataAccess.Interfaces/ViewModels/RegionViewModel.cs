using Northwind.Mvvm;
using System.Collections.ObjectModel;

namespace Northwind.ViewModels
{
    public class RegionViewModel : EntityViewModel<Data.Region>
    {
        private int? _regionId;
        private string? _description = string.Empty;
        private ObservableCollection<TerritoryViewModel>? _territories;
        public int? RegionId
        {
            get => _regionId;
            set => SetProperty(ref _regionId, value);
        }
        public string? Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        public ObservableCollection<TerritoryViewModel> Territories
        {
            get => _territories ??= [];
            set => SetProperty(ref _territories, value);
        }

        public string DescriptionExtended => $"{Description?.Trim()} Region";
    }
}
