using Northwind.Data;
using Prism.Ioc;

namespace Northwind.DataAccess
{
    public class ShipperUnitOfWork(IContainerExtension container) : UnitOfWorkBase<Shipper>(container), IShipperUnitOfWork
    {
    }
}
