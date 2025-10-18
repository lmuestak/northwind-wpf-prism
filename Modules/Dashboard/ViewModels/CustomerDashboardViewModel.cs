using Northwind.Data;
using Northwind.ViewModels.Charts;
using Northwind.ViewModels.Dashboards;
using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.ViewModels
{
    public class CustomerDashboardViewModel : DashboardViewModelBase
    {
        private ObservableCollection<SalesReportData> _salesReportData;
        private CustomerRegionCartesianChartViewModel _customerRegionChart;
        private SalesByRegionCartesianChartViewModel _salesByRegionChart;
        private AnalyticsDashboardViewModel analyticsDashboardViewModel;

        public ObservableCollection<SalesReportData> SalesReportData
        {
            get => _salesReportData;
            set => SetProperty(ref _salesReportData, value);
        }
        public CustomerRegionCartesianChartViewModel CustomerRegionChart
        {
            get => _customerRegionChart ??= new CustomerRegionCartesianChartViewModel();
            set => SetProperty(ref _customerRegionChart, value);
        }

        public AnalyticsDashboardViewModel AnalyticsDashboard
        {
            get => analyticsDashboardViewModel;
            set => SetProperty(ref analyticsDashboardViewModel, value);
        }

        public SalesByRegionCartesianChartViewModel SalesByRegionChart
        {
            get => _salesByRegionChart ??= new SalesByRegionCartesianChartViewModel();
            set => SetProperty(ref _salesByRegionChart, value);
        }

        public override Expression<Func<SalesReportData, bool>> CreateFilter()
        {
            Expression<Func<SalesReportData, bool>> filter = s => true; // Start with 'always true'

            if (Region != null && Region.RegionId.HasValue)
                filter = filter.And(s => s.RegionId == Region.RegionId.Value);

            if (Territory != null && !string.IsNullOrEmpty(Territory.TerritoryId))
                filter = filter.And(s => s.EmployeeTerritoryId == Territory.TerritoryId);

            if (Customer != null && !string.IsNullOrEmpty(Customer.CustomerId))
                filter = filter.And(s => s.CustomerId == Customer.CustomerId);

            if (Category != null && Category.CategoryId.HasValue)
                filter = filter.And(s => s.CategoryId == Category.CategoryId.Value);

            if (StartDate.IsValid())
                filter = filter.And(s => s.OrderAt >= StartDate);

            if (EndDate.IsValid())
                filter = filter.And(s => s.OrderAt <= EndDate);

            return filter;
        }

        public override Task OnPrepareDashboardDataAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public override async Task OnLoadFilterDataAsync(CancellationToken cancellationToken = default)
        {
            await GetRegionsAsync(cancellationToken);
            await GetCategoriesAsync(cancellationToken);
            await GetCustomersAsync(cancellationToken);
            await GetDatesAsync(7, cancellationToken);
        }
       
    }
}
