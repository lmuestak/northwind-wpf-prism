using Northwind.Data;

namespace Northwind.ViewModels
{
    public static class SupplierViewModelExtensions
    {
        public static SupplierViewModel ToViewModel(this Supplier supplier)
        {
            ArgumentNullException.ThrowIfNull(supplier);
            return new SupplierViewModel()
            {
                SupplierId = supplier.SupplierId,
                CompanyName = supplier.CompanyName ?? string.Empty,
                ContactName = supplier.ContactName ?? string.Empty,
                ContactTitle = supplier.ContactTitle ?? string.Empty,
                Address = supplier.Address ?? string.Empty,
                City = supplier.City ?? string.Empty,
                Region = supplier.Region ?? string.Empty,
                PostalCode = supplier.PostalCode ?? string.Empty,
                Country = supplier.Country ?? string.Empty,
                Phone = supplier.Phone ?? string.Empty,
                Fax = supplier.Fax ?? string.Empty,
                HomePage = supplier.HomePage ?? string.Empty
            };
        }
        public static Supplier ToEntity(this SupplierViewModel viewModel)
        {
            ArgumentNullException.ThrowIfNull(viewModel);
            return new Supplier
            {
                SupplierId = viewModel.SupplierId.GetValueOrDefault(),
                CompanyName = viewModel.CompanyName,
                ContactName = viewModel.ContactName ?? string.Empty,
                ContactTitle = viewModel.ContactTitle ?? string.Empty,
                Address = viewModel.Address ?? string.Empty,
                City = viewModel.City ?? string.Empty,
                Region = viewModel.Region ?? string.Empty,
                PostalCode = viewModel.PostalCode ?? string.Empty,
                Country = viewModel.Country ?? string.Empty,
                Phone = viewModel.Phone ?? string.Empty,
                Fax = viewModel.Fax ?? string.Empty,
                HomePage = viewModel.HomePage ?? string.Empty
            };
        }
    }

}
