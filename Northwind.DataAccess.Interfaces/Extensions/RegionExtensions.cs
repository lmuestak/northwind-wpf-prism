using System.Collections.ObjectModel;

namespace Northwind.ViewModels
{
    public static class RegionExtensions
    {
        public static RegionViewModel ToViewModel(this Data.Region region)
        {
            ArgumentNullException.ThrowIfNull(region);
            return new RegionViewModel()
            {
                RegionId = region.RegionId,
                Description = region.Description,
                Territories = region.Territories != null ? new ObservableCollection<TerritoryViewModel>(region.Territories.Select(t => t.ToViewModel())) : [],
                State = Mvvm.EntityState.Unchanged
                
            };
        }
        public static Data.Region ToEntity(this RegionViewModel viewModel)
        {
            ArgumentNullException.ThrowIfNull(viewModel);
            return new Data.Region
            {
                RegionId = viewModel.RegionId.GetValueOrDefault(),
                Territories = viewModel.Territories?.Select(t => t.ToEntity()).ToList() ?? [],
                Description = viewModel.Description
            };
        }
    }
}
