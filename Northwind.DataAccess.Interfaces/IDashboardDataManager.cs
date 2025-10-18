using System.Linq.Expressions;

namespace Northwind.DataAccess
{
    public enum TrackingMode
    {
        Track,
        NoTracking
    }

    public sealed record QuerySpecification<T>(Expression<Func<T, bool>>? Filter = null,Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy = null, int? Skip = null, int? Take = null, TrackingMode Tracking = TrackingMode.NoTracking, Expression<Func<T, object>>[]? Includes = null) where T : class;

    public interface IDashboardDataManager : IAsyncDisposable, IDisposable
    {
        Task<IReadOnlyList<T>> GetAsync<T>(QuerySpecification<T> spec, CancellationToken cancellationToken = default) where T : class;

        IAsyncEnumerable<T> StreamAsync<T>(QuerySpecification<T> spec, CancellationToken cancellationToken = default) where T : class;
    }

    /*
     * 
    
    1. Basic read (materialized list)

        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        var active = await dataManager.GetAsync<User>(filter: u => u.IsActive, orderBy: q => q.OrderBy(u => u.LastName).ThenBy(u => u.FirstName), skip: 0, take: 50, tracking: TrackingMode.NoTracking, includes: new Expression<Func<User, object>>[] { u => u.Organization }, cancellationToken: cts.Token);
    
    
    2. Streaming a large dataset

    await foreach (var log in dataManager.StreamAsync<AuditLog>(filter: l => l.CreatedUtc >= DateTime.UtcNow.AddDays(-7), orderBy: q => q.OrderByDescending(l => l.CreatedUtc), tracking: TrackingMode.NoTracking, includes: null, cancellationToken: cancellationToken))
    {
        Process(log);
    }
 
    3. With paging encapsulated

    var pageIndex = 3; // 0-based
    var pageSize  = 25;
    var orders = await dataManager.GetAsync<Order>(filter: o => o.Status == OrderStatus.Open,orderBy: q => q.OrderByDescending(o => o.CreatedAt), skip: pageIndex * pageSize, take: pageSize, tracking: TrackingMode.NoTracking, includes: new Expression<Func<Order, object>>[] { o => o.Customer, o => o.Items }, cancellationToken: cancellationToken);
 
    4. Projection to a lightweight DTO (avoid over-fetching)
    
    // If you add a projection variant later, you can keep callers lean:
    var recent = await dbContext.Set<Order>() // sometimes direct query is fine inside app layer
    .Where(o => o.CreatedAt >= DateTime.UtcNow.AddDays(-30)).OrderByDescending(o => o.CreatedAt).Select(o => new OrderListItem(o.Id, o.Number, o.Total, o.Customer.Name)).ToListAsync(cancellationToken);

   5. Using the QuerySpec<T> form (if you pick that design)

    var spec = new QuerySpec<User>(Filter: u => u.EmailVerified,OrderBy: q => q.OrderBy(u => u.LastLoginUtc), Skip: 0, Take: 100, Tracking: TrackingMode.NoTracking, Includes: new Expression<Func<User, object>>[] { u => u.Roles });
    var users = await dataManager.GetAsync(spec, cancellationToken);

*/
}
