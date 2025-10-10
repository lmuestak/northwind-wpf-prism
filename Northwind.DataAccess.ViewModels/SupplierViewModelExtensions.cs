namespace Northwind.Data
{
    public static class SupplierViewModelExtensions
    {
        public static SupplierViewModel ToViewModel(this Supplier supplier, IContainerExtension container)
        {
            ArgumentNullException.ThrowIfNull(supplier);
            ArgumentNullException.ThrowIfNull(container);
            return new SupplierViewModel(container)
            {
                SupplierId = supplier.SupplierId,
                CompanyName = supplier.CompanyName,
                ContactName = supplier.ContactName,
                ContactTitle = supplier.ContactTitle,
                Address = supplier.Address,
                City = supplier.City,
                Region = supplier.Region,
                PostalCode = supplier.PostalCode,
                Country = supplier.Country,
                Phone = supplier.Phone,
                Fax = supplier.Fax,
                HomePage = supplier.HomePage
            };
        }
        public static Supplier ToEntity(this SupplierViewModel viewModel)
        {
            ArgumentNullException.ThrowIfNull(viewModel);
            return new Supplier
            {
                SupplierId = viewModel.SupplierId,
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
