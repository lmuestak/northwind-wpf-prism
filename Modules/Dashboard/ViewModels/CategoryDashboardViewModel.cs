using Northwind.Data;
using Northwind.ViewModels.Dashboards;
using Northwind.ViewModels.Widgets;
using Prism.Ioc;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.ViewModels
{
    public class CategoryDashboardViewModel : DashboardViewModelBase
    {
        private SalesByCategoryPieChartWidgetViewModel _salesByCategoryPieChartWidgetViewModel;
        public SalesByCategoryPieChartWidgetViewModel SalesByCategory
        {
            get => _salesByCategoryPieChartWidgetViewModel ??=  Container.Resolve<SalesByCategoryPieChartWidgetViewModel>();
            set => SetProperty(ref _salesByCategoryPieChartWidgetViewModel, value);
        }

        public override Expression<Func<SalesReportData, bool>> CreateFilter()
        {
            Expression<Func<SalesReportData, bool>> filter = s => true; // Start with 'always true'
            if (StartDate.IsValid())
                filter = filter.And(s => s.OrderAt >= StartDate);
            if (EndDate.IsValid())
                filter = filter.And(s => s.OrderAt <= EndDate);
            return filter;
        }

        public override async Task OnLoadFilterDataAsync(CancellationToken cancellationToken = default)
        {
            await GetDatesAsync(7, cancellationToken);
        }

        public override async Task OnPrepareDashboardDataAsync(CancellationToken cancellationToken = default)
        {
            Task.Delay(100, cancellationToken).Wait(cancellationToken);
            await SalesByCategory.PrepareData(Data);
        }  
    }
}
