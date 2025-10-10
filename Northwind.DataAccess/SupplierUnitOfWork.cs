using Northwind.Data;
using Prism.Ioc;

namespace Northwind.DataAccess
{
    public class SupplierUnitOfWork(IContainerExtension container) : UnitOfWorkBase<Supplier>(container), ISupplierUnitOfWork
    {
    }
}
