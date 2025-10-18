using LiveCharts;
using LiveCharts.Wpf;
using Northwind.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.ViewModels.Widgets
{

    public class SalesByCategoryPieChartWidgetViewModel : WidgetViewModelBase<SalesReportData>
    {
        
        public SalesByCategoryPieChartWidgetViewModel(): base()
        {

            Title = "Sales by Category";
            IsLoading = true;
            InnerRadius = 40;
        }

        public override Task PrepareData(IList<SalesReportData> data, CancellationToken cancellationToken = default)
        {
            try
            {
                IsLoading = true;
                var categorySales = data
                    .Where(s => s.CategoryName != null && s.UnitPrice.HasValue && s.Quantity.HasValue)
                    .GroupBy(s => s.CategoryName)
                    .Select(g => new
                    {
                        CategoryName = g.Key,
                        TotalSales = g.Sum(s => s.UnitPrice.Value * s.Quantity.Value * (1 - (decimal?)(s.Discount ?? 0)))
                    })
                    .OrderByDescending(cs => cs.TotalSales)
                    .ToList();
                Labels = [.. categorySales.Select(cs => cs.CategoryName)];
                Series = [];
                foreach (var category in categorySales)
                {
                    Series.Add(new PieSeries
                    {
                        Title = category.CategoryName,
                        Values = new ChartValues<decimal> { category.TotalSales.GetValueOrDefault() },
                        DataLabels = true,
                        LabelPosition = PieLabelPosition.InsideSlice,
                        //LabelPoint = chartPoint => $"{chartPoint.Y:C2} ({chartPoint.Participation:P})"
                        LabelPoint = chartPoint => $"{chartPoint.Participation:P2}"
                    });
                }

                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
