using Northwind.Data;
using Prism.Ioc;

namespace Northwind.DataAccess
{
    public class OrderUnitOfWork(IContainerExtension container) : UnitOfWorkBase<Order>(container), IOrderUnitOfWork
    {
    }
}
