using LiveCharts;
using LiveCharts.Wpf;
using Northwind.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.ViewModels.Charts
{
    public class CustomerRegionCartesianChartViewModel : CartesianChartViewModelBase
    {
        public CustomerRegionCartesianChartViewModel() : base("Customers by Region")
        {
            ShowLabels = true;
            YFormatter = value => value.ToString("N0");
        }
        public override Task Transform(IEnumerable<SalesReportData> rows)
        {
            // Normalize and shape data
            var data = rows.Select(r => new
            {
                Region = string.IsNullOrWhiteSpace(r.RegionName) ? "Unknown" : r.RegionName,
                Territory = string.IsNullOrWhiteSpace(r.TerritoryName) ? "Unknown" : r.TerritoryName,
                UnitPrice = r.UnitPrice ?? 0m,
                Quantity = (decimal)(r.Quantity ?? 0),
                Discount = (decimal)(r.Discount ?? 0f),
                SalesPrice = (r.UnitPrice ?? 0m) * (decimal)(r.Quantity ?? 0),
                DiscountAmount = (r.UnitPrice ?? 0m) * (decimal)(r.Quantity ?? 0) * (decimal)(r.Discount ?? 0f),
                EndPrice = (r.UnitPrice ?? 0m) * (decimal)(r.Quantity ?? 0) * (1 - (decimal)(r.Discount ?? 0f))
            }).ToList();

            // Regions go on Y axis (right)
            var regionsList = data.Select(d => d.Region).Distinct().OrderBy(s => s).ToList();
            if (regionsList.Count > 1)
            {
                Labels = [.. regionsList];

                // Each territory is a stacked series
                var territories = data.Select(d => d.Territory).Distinct().OrderBy(s => s).ToList();

                Series = [];

                foreach (var territory in territories)
                {
                    // One value per region (same order as Regions array)
                    var values = regionsList.Select(region =>
                    {
                        var total = data
                            .Where(d => d.Region == region && d.Territory == territory)
                            .Sum(d => d.EndPrice);
                        return (double)total;
                    }).ToArray();

                    Series.Add(new StackedRowSeries
                    {
                        Title = territory,
                        Values = new ChartValues<double>(values),
                        DataLabels = false
                    });
                }
            }
            else
            {
                Series = [];
                var territories = data.Select(d => d.Territory).Distinct().OrderBy(s => s).ToList();
                Labels = [.. regionsList];

                // Each territory is a stacked series
                

                Series = [];

                foreach (var territory in territories)
                {
                    // One value per region (same order as Regions array)
                    var values = regionsList.Select(region =>
                    {
                        var total = data
                            .Where(d => d.Region == region && d.Territory == territory)
                            .Sum(d => d.EndPrice);
                        return (double)total;
                    }).ToArray();

                    Series.Add(new StackedRowSeries
                    {
                        Title = territory,
                        Values = new ChartValues<double>(values),
                        DataLabels = false
                    });
                }
            }
            return Task.CompletedTask;
        }
    }
}
