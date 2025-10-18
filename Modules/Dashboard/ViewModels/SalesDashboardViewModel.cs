using Northwind.ViewModels.Dashboards;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.ViewModels
{

    public class SalesDashboardViewModel : DashboardViewModelBase
    {

        public override async Task OnLoadFilterDataAsync(CancellationToken cancellationToken = default)
        {
            await GetRegionsAsync(cancellationToken);
            await GetDatesAsync(7,cancellationToken);
            await GetCategoriesAsync(cancellationToken);
            await GetEmployeesAsync(cancellationToken);
            await GetCustomersAsync(cancellationToken);
            await GetShippersAsync(cancellationToken);
            await GetSuppliersAsync(cancellationToken);
        }

        public override Task OnLoadDashboardDataAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public override Task OnPrepareDashboardDataAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }

}
