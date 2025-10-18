using Northwind.Data;

namespace Northwind.ViewModels
{
    public static class ShipperViewModelExtensions
    {
        public static ShipperViewModel ToViewModel(this Shipper shipper)
        {
            ArgumentNullException.ThrowIfNull(shipper);
            return new ShipperViewModel()
            {
                ShipperId = shipper.ShipperId,
                CompanyName = shipper.CompanyName,
                Phone = shipper.Phone
            };
        }
        public static Shipper ToEntity(this ShipperViewModel viewModel)
        {
            ArgumentNullException.ThrowIfNull(viewModel);
            return new Shipper
            {
                ShipperId = viewModel.ShipperId.GetValueOrDefault(),
                CompanyName = viewModel.CompanyName,
                Phone = viewModel.Phone ?? string.Empty
            };
        }
    }
}
