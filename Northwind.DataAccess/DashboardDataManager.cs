using Microsoft.EntityFrameworkCore;
using Northwind.Data;
using Northwind.Services.Interfaces;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.DataAccess
{
    public class DashboardDataManager: IDashboardDataManager
    {
        private bool _disposed = false;
        private readonly NorthwindDbContext _context;
        private readonly IContainerExtension _container;
        public DashboardDataManager(IContainerExtension container)
        {
            Container = container;
            var connectionString = ConfigurationService.GetConnectionString();
            var dbProvider = ConfigurationService.GetDbProvider();
            var optionsBuilder = new DbContextOptionsBuilder<NorthwindDbContext>();
            if (!string.IsNullOrEmpty(connectionString))
            {
                if (dbProvider.Equals("sqlite", StringComparison.OrdinalIgnoreCase))
                {
                    optionsBuilder.UseSqlite(connectionString);
                }
                else if (dbProvider.Equals("mssql", StringComparison.OrdinalIgnoreCase) || dbProvider.Equals("sqlserver", StringComparison.OrdinalIgnoreCase))
                {
                    optionsBuilder.UseSqlServer(connectionString);
                }
                else
                {
                    throw new InvalidOperationException($"Unsupported database provider: {dbProvider}");
                }
            }
            else
            {
                throw new InvalidOperationException("Connection string is not provided.");
            }
            _context = new NorthwindDbContext(optionsBuilder.Options);
        }
        public IContainerExtension Container { get; private set; }
        public NorthwindDbContext Context => _context;
        public IConfigurationService ConfigurationService => Container.Resolve<IConfigurationService>();

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public Task<ObservableCollection<SalesOverviewByCategory>> GetSalesOverviewByCategoriesAsync()
        {
            var q = Context.Categories
                .Join(Context.Products, c => c.CategoryId, p => p.CategoryId, (c, p) => new { c, p })
                .Join(Context.ExtendedOrderDetails, cp => cp.p.ProductId, ode => ode.ProductId, (cp, ode) => new { cp.c, cp.p, ode })
                .Join(Context.Orders, cpo => cpo.ode.OrderId, o => o.OrderId, (cpo, o) => new { cpo.c, cpo.p, cpo.ode, o })
                .Select(x => new
                {
                    x.c.CategoryId,
                    x.p.ProductId,
                    x.o.OrderId,
                    x.c.CategoryName,
                    x.p.ProductName,
                    x.o.OrderDate,
                    x.ode.UnitPrice,
                    x.ode.Quantity,
                    x.ode.Discount
                });
            return Task.FromResult(new ObservableCollection<SalesOverviewByCategory>(q.AsEnumerable().Select(x => new SalesOverviewByCategory
            {
                CategoryId = x.CategoryId,
                ProductId = x.ProductId,
                OrderId = x.OrderId,
                CategoryName = x.CategoryName,
                ProductName = x.ProductName,
                OrderDate = x.OrderDate,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity,
                DiscountPercent = x.Discount
            })));
        }

        public Task<ObservableCollection<SalesOverviewByEmployee>> GetSalesOverviewByEmployeesAsync()
        {
            var sales = Context.SalesOverviewByEmployees.AsNoTracking().ToList();
            return Task.FromResult(new ObservableCollection<SalesOverviewByEmployee>(sales));
        }
    }
}
