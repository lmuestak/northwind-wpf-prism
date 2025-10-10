using System.Linq.Expressions;

namespace Northwind.DataAccess
{
    public interface IUnitOfWork<T> : IDisposable where T : class
    {
        IEnumerable<T> Get(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(object id);
        void Delete(T entityToDelete);
        int SaveChanges();
        Task SaveChangesAsync();

    }
}
