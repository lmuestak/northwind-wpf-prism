using Northwind.Data;
using Prism.Ioc;

namespace Northwind.DataAccess
{
    public class CustomerUnitOfWork(IContainerExtension container) : UnitOfWorkBase<Customer>(container), ICustomerUnitOfWork
    {
    }
}
