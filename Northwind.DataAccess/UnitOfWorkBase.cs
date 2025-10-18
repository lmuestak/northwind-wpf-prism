using Microsoft.EntityFrameworkCore;
using Northwind.Data;
using Northwind.Services.Interfaces;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Northwind.DataAccess
{
    public class UnitOfWorkBase<T> : IUnitOfWork<T> where T : class
    {

        private readonly NorthwindDbContext _context;
        private readonly DbSet<T> _dbSet;
        private bool _disposed = false;

        public UnitOfWorkBase(IContainerExtension container)
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
            _dbSet = _context.Set<T>();
        }

        /*
        public void RegisterEntities(IContainerRegistry register)
        {
            // Register Entities
            if (!register.IsRegistered<Category>())
            {
                Container.Register<Category>();
            }
            if (!register.IsRegistered<Product>())
            {
                Container.Register<Product>();
            }
            if (!register.IsRegistered<Customer>())
            {
                Container.Register<Customer>();
            }
            if (!register.IsRegistered<Employee>())
            {
                Container.Register<Employee>();
            }
            if (!register.IsRegistered<Order>())
            {
                Container.Register<Order>();
            }
            if (!register.IsRegistered<Supplier>())
            {
                Container.Register<Supplier>();
            }
            if (!register.IsRegistered<Shipper>())
            {
                Container.Register<Shipper>();
            }
            if (!register.IsRegistered<Territory>())
            {
                Container.Register<Territory>();
            }
            if (!register.IsRegistered<Region>())
            {
                Container.Register<Region>();
            }
            if (!register.IsRegistered<OrderDetail>())
            {
                Container.Register<OrderDetail>();
            }
            if (!register.IsRegistered<EmployeeTerritory>())
            {
                Container.Register<EmployeeTerritory>();
            }
            // Register ViewModels
            if (!register.IsRegistered<CategoryViewModel>())
            {
                Container.Register<CategoryViewModel>();
            }
            if (!register.IsRegistered<ProductViewModel>())
            {
                Container.Register<ProductViewModel>();
            }
            if (!register.IsRegistered<CustomerViewModel>())
            {
                Container.Register<CustomerViewModel>();
            }
            if (!register.IsRegistered<EmployeeViewModel>())
            {
                Container.Register<EmployeeViewModel>();
            }
            if (!register.IsRegistered<OrderViewModel>())
            {
                Container.Register<OrderViewModel>();
            }
            if (!register.IsRegistered<SupplierViewModel>())
            {
                Container.Register<SupplierViewModel>();
            }
            if (!register.IsRegistered<ShipperViewModel>())
            {
                Container.Register<ShipperViewModel>();
            }
            if (!register.IsRegistered<TerritoryViewModel>())
            {
                Container.Register<TerritoryViewModel>();
            }
            if (!register.IsRegistered<RegionViewModel>())
            {
                Container.Register<RegionViewModel>();
            }
            if (!register.IsRegistered<OrderDetailViewModel>())
            {
                Container.Register<OrderDetailViewModel>();
            }
            if (!register.IsRegistered<EmployeeTerritoryViewModel>())
            {
                Container.Register<EmployeeTerritoryViewModel>();
            }
        }
        */
        public IContainerExtension Container { get; private set; }

        public IConfigurationService ConfigurationService => Container.Resolve<IConfigurationService>();

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[','], StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual T GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual void Update(T entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

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

        public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[','], StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return Task.FromResult<IEnumerable<T>>(orderBy(query).ToList());
            }
            else
            {
                return Task.FromResult<IEnumerable<T>>(query.ToList());
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

    }
}
