using Northwind.Data;
using Prism.Ioc;

namespace Northwind.DataAccess
{
    public class EmployeeUnitOfWork(IContainerExtension container) : UnitOfWorkBase<Employee>(container), IEmployeeUnitOfWork
    {
    }
}
