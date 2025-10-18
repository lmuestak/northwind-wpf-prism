using Northwind.Data;
using Northwind.Mvvm;

namespace Northwind.ViewModels
{
    public class TerritoryViewModel : EntityViewModel<Territory>
    {
        private string? _territoryId = string.Empty;
        private string? _description = string.Empty;
        private int? _regionId;

        public string? TerritoryId
        {
            get => _territoryId;
            set => SetProperty(ref _territoryId, value);
        }
        public string? Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        public int? RegionId
        {
            get => _regionId;
            set => SetProperty(ref _regionId, value);
        }
        public string DescriptionExtended => $"{Description?.Trim()} Territory";
    }
}
