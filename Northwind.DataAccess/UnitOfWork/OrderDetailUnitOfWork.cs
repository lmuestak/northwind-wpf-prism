using Northwind.Data;
using Prism.Ioc;

namespace Northwind.DataAccess
{
    public class OrderDetailUnitOfWork(IContainerExtension container) : UnitOfWorkBase<OrderDetail>(container), IOrderDetailUnitOfWork
    {
    }
}
