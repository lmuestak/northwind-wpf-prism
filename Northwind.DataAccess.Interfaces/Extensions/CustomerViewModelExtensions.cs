using Northwind.Data;

namespace Northwind.ViewModels
{
    public static class CustomerViewModelExtensions
    {
        public static CustomerViewModel ToViewModel(this Customer customer)
        {
            ArgumentNullException.ThrowIfNull(customer);
            return new CustomerViewModel()
            {
                CustomerId = customer.CustomerId,
                CompanyName = customer.CompanyName,
                ContactName = customer.ContactName,
                ContactTitle = customer.ContactTitle,
                Address = customer.Address,
                City = customer.City,
                Region = customer.Region,
                PostalCode = customer.PostalCode,
                Country = customer.Country,
                Phone = customer.Phone,
                Fax = customer.Fax
            };
        }
        public static Customer ToEntity(this CustomerViewModel viewModel)
        {
            ArgumentNullException.ThrowIfNull(viewModel);
            return new Customer
            {
                CustomerId = viewModel.CustomerId,
                CompanyName = viewModel.CompanyName,
                ContactName = viewModel.ContactName ?? string.Empty,
                ContactTitle = viewModel.ContactTitle ?? string.Empty,
                Address = viewModel.Address ?? string.Empty,
                City = viewModel.City ?? string.Empty,
                Region = viewModel.Region ?? string.Empty,
                PostalCode = viewModel.PostalCode ?? string.Empty,
                Country = viewModel.Country ?? string.Empty,
                Phone = viewModel.Phone ?? string.Empty,
                Fax = viewModel.Fax ?? string.Empty
            };
        }
    }
}
