using LiveCharts;
using LiveCharts.Wpf;
using Northwind.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.ViewModels.Charts
{
    public class SalesByRegionCartesianChartViewModel : CartesianChartViewModelBase
    {
        public SalesByRegionCartesianChartViewModel() : base("Sales by Region")
        {
            ShowLabels = true;
            YFormatter = value => value.ToString("N2");
        }
        public override Task Transform(IEnumerable<SalesReportData> rows)
        {
            var data = rows.Select(r => new
            {
                RegionName = string.IsNullOrWhiteSpace(r.RegionName) ? "Unknown" : r.RegionName,
                UnitPrice = r.UnitPrice ?? 0m,
                Quantity = (decimal)(r.Quantity ?? 0),
                Discount = (decimal)(r.Discount ?? 0f),
                SalesPrice = (r.UnitPrice ?? 0m) * (decimal)(r.Quantity ?? 0),
                DiscountAmount = (r.UnitPrice ?? 0m) * (decimal)(r.Quantity ?? 0) * (decimal)(r.Discount ?? 0f),
                EndPrice = (r.UnitPrice ?? 0m) * (decimal)(r.Quantity ?? 0) * (1 - (decimal)(r.Discount ?? 0f))
            }).ToList();

            var regions = data.GroupBy(_ => _.RegionName).ToList();
            var Labels = regions.Select(d => d.Key).Distinct().OrderBy(s => s).ToList();
            Series = [];

            foreach (var region in regions)
            {
                var sales = region.Sum(d => d.EndPrice);
                Series.Add(new ColumnSeries
                {
                    Title = region.Key,
                    Values = new ChartValues<decimal>() { sales },
                    DataLabels = false
                });
            }
            return Task.CompletedTask;
        }
    }
}
