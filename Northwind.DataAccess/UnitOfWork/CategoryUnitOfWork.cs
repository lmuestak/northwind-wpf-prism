using Northwind.Data;
using Prism.Ioc;

namespace Northwind.DataAccess
{
    public class CategoryUnitOfWork(IContainerExtension container) : UnitOfWorkBase<Category>(container), ICategoryUnitOfWork
    {
    }
}
