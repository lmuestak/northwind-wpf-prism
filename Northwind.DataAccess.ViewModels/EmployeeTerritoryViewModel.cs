using Northwind.Mvvm;

namespace Northwind.Data
{
    public class EmployeeTerritoryViewModel(IContainerExtension container) : EntityViewModel<EmployeeTerritory>(container)
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