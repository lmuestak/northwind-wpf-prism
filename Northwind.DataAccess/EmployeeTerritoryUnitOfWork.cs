using Northwind.Data;
using Prism.Ioc;

namespace Northwind.DataAccess
{
    public class EmployeeTerritoryUnitOfWork(IContainerExtension container) : UnitOfWorkBase<EmployeeTerritory>(container), IEmployeeTerritoryUnitOfWork
    {
    }
}
