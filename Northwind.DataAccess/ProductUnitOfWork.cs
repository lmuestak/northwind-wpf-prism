using Northwind.Data;
using Prism.Ioc;

namespace Northwind.DataAccess
{
    public class ProductUnitOfWork(IContainerExtension container) : UnitOfWorkBase<Product>(container), IProductUnitOfWork
    {
    }
}
