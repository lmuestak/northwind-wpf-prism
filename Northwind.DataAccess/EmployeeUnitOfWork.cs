using Northwind.Data;
using Prism.Ioc;

namespace Northwind.DataAccess
{
    public class EmployeeUnitOfWork : UnitOfWorkBase<Employee>, IEmployeeUnitOfWork
    {
        public EmployeeUnitOfWork(IContainerExtension container) : base(container)
        {
        }
    }
}
