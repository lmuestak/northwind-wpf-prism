using Northwind.Mvvm;

namespace Northwind.Data
{
    public static class EmployeeTerritoryViewModelExtensions
    {
        public static EmployeeTerritoryViewModel ToViewModel(this EmployeeTerritory employeeTerritory, IContainerExtension container)
        {

            ArgumentNullException.ThrowIfNull(employeeTerritory);
            ArgumentNullException.ThrowIfNull(container);

            return new EmployeeTerritoryViewModel(container)
            {
                EmployeeId = employeeTerritory.EmployeeId,
                TerritoryId = employeeTerritory.TerritoryId,
                State = EntityState.Unchanged
            };

        }
        public static EmployeeTerritory ToEntity(this EmployeeTerritoryViewModel viewModel)
        {
            
            ArgumentNullException.ThrowIfNull(viewModel);

            return new EmployeeTerritory
            {
                EmployeeId = viewModel.EmployeeId,
                TerritoryId = viewModel.TerritoryId
            };

        }
    }
}
