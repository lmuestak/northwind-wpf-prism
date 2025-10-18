using Northwind.Data;

namespace Northwind.ViewModels
{
    public static class TerritoryExtensions
    {
        public static TerritoryViewModel ToViewModel(this Territory territory)
        {
            ArgumentNullException.ThrowIfNull(territory);
            return new TerritoryViewModel()
            {
                TerritoryId = territory.TerritoryId,
                RegionId = territory.RegionID,
                Description = territory.Description,
            };
        }
        public static Territory ToEntity(this TerritoryViewModel viewModel)
        {
            ArgumentNullException.ThrowIfNull(viewModel);
            return new Territory
            {
                RegionID = viewModel.RegionId.GetValueOrDefault(),
                TerritoryId = viewModel.TerritoryId,
                Description = viewModel.Description
            };
        }
    }
}
