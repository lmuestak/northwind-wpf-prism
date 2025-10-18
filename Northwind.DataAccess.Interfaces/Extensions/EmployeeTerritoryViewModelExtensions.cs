using Northwind.Data;
using Northwind.Mvvm;

namespace Northwind.ViewModels
{
    public static class EmployeeTerritoryViewModelExtensions
    {
        public static EmployeeTerritoryViewModel ToViewModel(this EmployeeTerritory employeeTerritory)
        {

            ArgumentNullException.ThrowIfNull(employeeTerritory);

            return new EmployeeTerritoryViewModel()
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
