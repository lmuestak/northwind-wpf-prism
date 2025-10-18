using Northwind.Data;
using Northwind.Mvvm;

namespace Northwind.ViewModels
{
    public class EmployeeTerritoryViewModel: EntityViewModel<EmployeeTerritory>
    {
        private int _employeeId;
        private string _territoryId = string.Empty;
        public int EmployeeId
        {
            get => _employeeId;
            set => SetProperty(ref _employeeId, value);
        }
        public string TerritoryId
        {
            get => _territoryId;
            set => SetProperty(ref _territoryId, value);
        }
    }
}