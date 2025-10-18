using Northwind.Data;

namespace Northwind.ViewModels
{
    public static class EmployeeViewModelExtensions
    {
        public static EmployeeViewModel ToViewModel(this Employee entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            return new EmployeeViewModel()
            {
                EmployeeId = entity.EmployeeId,
                LastName = entity.LastName,
                FirstName = entity.FirstName,
                Title = entity.Title ?? string.Empty,
                TitleOfCourtesy = entity.TitleOfCourtesy ?? string.Empty,
                BirthDate = entity.BirthDate,
                HireDate = entity.HireDate,
                Address = entity.Address,
                City = entity.City,
                Region = entity.Region,
                PostalCode = entity.PostalCode,
                Country = entity.Country,
                HomePhone = entity.HomePhone,
                Extension = entity.Extension,
                Photo = entity.Photo,
                Notes = entity.Notes,
                ReportsTo = entity.ReportsTo,
                PhotoPath = entity.PhotoPath
            };
        }
        public static Employee ToEntity(this EmployeeViewModel viewModel)
        {
            ArgumentNullException.ThrowIfNull(viewModel);
            return new Employee
            {
                EmployeeId = viewModel.EmployeeId,
                LastName = viewModel.LastName ?? string.Empty,
                FirstName = viewModel.FirstName ?? string.Empty,
                Title = viewModel.Title ?? string.Empty,
                TitleOfCourtesy = viewModel.TitleOfCourtesy ?? string.Empty,
                BirthDate = viewModel.BirthDate,
                HireDate = viewModel.HireDate,
                Address = viewModel.Address ?? string.Empty,
                City = viewModel.City ?? string.Empty,
                Region = viewModel.Region ?? string.Empty,
                PostalCode = viewModel.PostalCode ?? string.Empty,
                Country = viewModel.Country ?? string.Empty,
                HomePhone = viewModel.HomePhone ?? string.Empty,
                Extension = viewModel.Extension ?? string.Empty,
                Photo = viewModel.Photo,
                Notes = viewModel.Notes ?? string.Empty,
                ReportsTo = viewModel.ReportsTo,
                PhotoPath = viewModel.PhotoPath ?? string.Empty
            };
        }
    }
}
