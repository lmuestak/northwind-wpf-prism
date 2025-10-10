using Northwind.Data;
using Prism.Ioc;

namespace Northwind.DataAccess
{
    public class RegionUnitOfWork(IContainerExtension container) : UnitOfWorkBase<Region>(container), IRegionUnitOfWork
    {
    }
}
