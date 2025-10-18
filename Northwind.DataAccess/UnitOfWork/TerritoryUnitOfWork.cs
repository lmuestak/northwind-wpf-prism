using Northwind.Data;
using Prism.Ioc;

namespace Northwind.DataAccess
{
    public class TerritoryUnitOfWork(IContainerExtension container) : UnitOfWorkBase<Territory>(container), ITerritoryUnitOfWork
    {
    }
}
