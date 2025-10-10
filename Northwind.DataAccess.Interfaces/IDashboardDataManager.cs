using Northwind.Data;
using System.Collections.ObjectModel;

namespace Northwind.DataAccess
{
    public interface IDashboardDataManager : IDisposable
    {
        Task<ObservableCollection<SalesOverviewByCategory>> GetSalesOverviewByCategoriesAsync();
        Task<ObservableCollection<SalesOverviewByEmployee>> GetSalesOverviewByEmployeesAsync();
    }
}
