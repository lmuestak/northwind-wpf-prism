using Microsoft.EntityFrameworkCore;
using Northwind.Data;
using Northwind.Services.Interfaces;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.DataAccess
{
    public class DashboardDataManager: IDashboardDataManager
    {
        private bool _disposed = false;
        private readonly NorthwindDbContext _context;
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
        public async Task<IReadOnlyList<T>> GetAsync<T>(QuerySpecification<T> specification, CancellationToken cancellationToken = default) where T : class
        {
            ArgumentNullException.ThrowIfNull(specification);

            IQueryable<T> query = BuildQuery(specification);

            // Materialize
            Debug.WriteLine(query.ToQueryString());
            var list = await query.ToListAsync(cancellationToken);
            return list;
        }

        public IAsyncEnumerable<T> StreamAsync<T>(QuerySpecification<T> specification, CancellationToken cancellationToken = default) where T : class
        {
            ArgumentNullException.ThrowIfNull(specification);
            IQueryable<T> query = BuildQuery(specification);

            // Stream without buffering the entire result set.
            // WithCancellation propagates the provided token during enumeration.
            return Enumerate(query, cancellationToken);
        }

        private static async IAsyncEnumerable<T> Enumerate<T>(IQueryable<T> query, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await foreach (var item in query.AsAsyncEnumerable().WithCancellation(cancellationToken))
            {
                yield return item;
            }
        }

        private IQueryable<T> BuildQuery<T>(QuerySpecification<T> specification) where T : class
        {
            IQueryable<T> query = Context.Set<T>();

            // Tracking mode
            query = specification.Tracking switch
            {
                TrackingMode.NoTracking => query.AsNoTracking(),
                TrackingMode.Track => query, // default tracking
                _ => query
            };

            // Includes
            if (specification.Includes is { Length: > 0 })
            {
                foreach (var include in specification.Includes)
                    query = query.Include(include);
            }

            // Filter
            if (specification.Filter is not null)
                query = query.Where(specification.Filter);

            // OrderBy
            if (specification.OrderBy is not null)
                query = specification.OrderBy(query);

            // Pagination (apply only if provided)
            if (specification.Skip is int skip && skip > 0)
                query = query.Skip(skip);

            if (specification.Take is int take && take > 0)
                query = query.Take(take);

            return query;
        }

        // Dispose/AsyncDispose simply delegate to the DbContext if needed.
        public ValueTask DisposeAsync()
        {
            // If your container controls DbContext lifetime, you might NO-OP here.
            if (_context is IAsyncDisposable ad) return ad.DisposeAsync();
            _context.Dispose();
            return ValueTask.CompletedTask;
        }

    }
}
