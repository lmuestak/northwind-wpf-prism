using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Northwind.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Northwind.ViewModels
{
    public class AnalyticsDashboardViewModel : INotifyPropertyChanged
    {
        // Customer Pareto
        public SeriesCollection CustomerParetoSeries { get; } = new SeriesCollection();
        public string[] CustomerLabels { get; }

        // Employee Quarterly
        public SeriesCollection EmployeeQuarterSeries { get; } = new SeriesCollection();
        public string[] EmployeeLabels { get; }

        // Regions & Territories
        public SeriesCollection RegionTerritorySeries { get; } = new SeriesCollection();
        public string[] RegionLabels { get; } = [];

        // Suppliers (Top N + Avg Discount)
        public SeriesCollection SupplierTopSeries { get; } = new SeriesCollection();
        public SeriesCollection SupplierDiscountSeries { get; } = new SeriesCollection();
        public string[] SupplierLabels { get; } = [];

        // Shippers (scatter: speed vs on-time)
        public SeriesCollection ShipperScatterSeries { get; } = new SeriesCollection();

        // Formatters
        public Func<double, string> CurrencyFormatter { get; }
        public Func<double, string> PercentFormatter { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public AnalyticsDashboardViewModel(IEnumerable<SalesReportData> rows)
        {
            var data = rows.Select(r => new
            {
                Customer = string.IsNullOrWhiteSpace(r.CustomerId) ? "Unknown" : r.CustomerId,
                Employee = $"Emp {r.EmployeeId}",
                Region = string.IsNullOrWhiteSpace(r.Region) ? "Unknown" : r.Region,
                Territory = string.IsNullOrWhiteSpace(r.EmployeeTerritoryId) ? "Unknown" : r.EmployeeTerritoryId,
                Supplier = $"Sup {r.SupplierId}",
                Shipper = $"Shp {r.ShipperId}",
                OrderAt = r.OrderAt,
                RequiredAt = r.RequiredAt,
                ShippedAt = r.ShippedAt,
                UnitPrice = r.UnitPrice ?? 0m,
                Qty = (decimal)(r.Quantity ?? 0),
                Discount = (decimal)(r.Discount ?? 0f)
            }).ToList();

            // Helper: sales amount
            decimal SaleOf(dynamic d) => d.UnitPrice * d.Qty * (1 - d.Discount);

            CurrencyFormatter = v => v.ToString("C0", CultureInfo.CurrentCulture);
            PercentFormatter = v => v.ToString("P0", CultureInfo.CurrentCulture);

            // ======================
            // 1) CUSTOMER PARETO
            // ======================
            var custAgg = data
                .GroupBy(d => d.Customer)
                .Select(g => new { Customer = g.Key, Sales = g.Sum(SaleOf) })
                .OrderByDescending(x => x.Sales)
                .ToList();

            CustomerLabels = custAgg.Select(x => x.Customer).ToArray();

            var custSales = custAgg.Select(x => (double)x.Sales).ToArray();
            var totalSales = custAgg.Sum(x => x.Sales);
            var cumulative = new List<double>(custAgg.Count);
            decimal running = 0;
            foreach (var x in custAgg)
            {
                running += x.Sales;
                cumulative.Add(totalSales == 0 ? 0 : (double)(running / totalSales));
            }

            CustomerParetoSeries =
            [
                new ColumnSeries { Title = "Sales", Values = new ChartValues<double>(custSales) },
                //new LineSeries
                //{
                //    Title = "Cumulative %",
                //    Values = new ChartValues<double>(cumulative),
                //    ScalesYAt = 1, // secondary Y axis
                //    //PointGeometry = null,
                //    DataLabels = false
                //}
            ];

            
            // =======================================
            // 2) EMPLOYEE SALES PERFORMANCE by QUARTER
            // =======================================
            var employees = data.Select(d => d.Employee).Distinct().OrderBy(s => s).ToList();
            EmployeeLabels = employees.ToArray();

            // Build stacked rows: one series per quarter (Q1..Q4)
            EmployeeQuarterSeries = new SeriesCollection();
            for (int q = 1; q <= 4; q++)
            {
                var qValues = new List<double>(employees.Count);
                foreach (var emp in employees)
                {
                    var sum = data
                        .Where(d => d.Employee == emp && QuarterOf(d.OrderAt) == q)
                        .Sum(SaleOf);
                    qValues.Add((double)sum);
                }

                EmployeeQuarterSeries.Add(new StackedRowSeries
                {
                    Title = $"Q{q}",
                    Values = new ChartValues<double>(qValues),
                    DataLabels = false
                });
            }
            // ====================================
            // 3) REGIONS & TERRITORIES (stacked)
            // ====================================
            var regions = data.Select(d => d.Region).Distinct().OrderBy(s => s).ToList();
            RegionLabels = regions.ToArray();
            var territories = data.Select(d => d.Territory).Distinct().OrderBy(s => s).ToList();

            RegionTerritorySeries = new SeriesCollection();
            foreach (var terr in territories)
            {
                var vals = regions.Select(reg =>
                    (double)data.Where(d => d.Region == reg && d.Territory == terr).Sum(SaleOf)
                ).ToArray();

                RegionTerritorySeries.Add(new StackedRowSeries
                {
                    Title = terr,
                    Values = new ChartValues<double>(vals),
                    DataLabels = false
                });
            }

            // ====================================
            // 4) SUPPLIERS (Top N + Avg Discount)
            // ====================================
            const int topN = 10;
            var supplierAgg = data
                .GroupBy(d => d.Supplier)
                .Select(g => new
                {
                    Supplier = g.Key,
                    Sales = g.Sum(SaleOf),
                    AvgDiscount = g.Any() ? (double)g.Average(x => x.Discount) : 0.0
                })
                .OrderByDescending(x => x.Sales)
                .ToList();

            var topSuppliers = supplierAgg.Take(topN).ToList();
            SupplierLabels = topSuppliers.Select(x => x.Supplier).ToArray();

            SupplierTopSeries = new SeriesCollection
        {
            new ColumnSeries
            {
                Title = "Sales",
                Values = new ChartValues<double>(topSuppliers.Select(x => (double)x.Sales))
            }
        };

            SupplierDiscountSeries = new SeriesCollection
        {
            new ColumnSeries
            {
                Title = "Avg Discount",
                Values = new ChartValues<double>(topSuppliers.Select(x => x.AvgDiscount))
            }
        };

            // ============================================
            // 5) SHIPPERS (Avg days to ship vs on-time %)
            // ============================================
            // On-time: ShippedAt <= RequiredAt (when both present)
            var shipperAgg = data
                .Where(d => d.ShippedAt.HasValue) // need shipped date for speed
                .GroupBy(d => d.Shipper)
                .Select(g =>
                {
                    var shipped = g.Where(x => x.ShippedAt.HasValue).ToList();
                    var avgDays = shipped.Any()
                        ? shipped.Average(x => (x.ShippedAt!.Value - x.OrderAt).TotalDays)
                        : 0.0;

                    var hasReq = shipped.Where(x => x.RequiredAt.HasValue).ToList();
                    var onTimeRate = hasReq.Any()
                        ? hasReq.Average(x => x.ShippedAt!.Value <= x.RequiredAt!.Value ? 1.0 : 0.0)
                        : 0.0;

                    var count = shipped.Count;

                    return new { Shipper = g.Key, AvgDays = avgDays, OnTime = onTimeRate, Count = count };
                })
                .OrderBy(x => x.AvgDays)
                .ToList();

            var scatter = new ScatterSeries
            {
                Title = "Shippers",
                MinPointShapeDiameter = 8,
                MaxPointShapeDiameter = 22,
                Values = new ChartValues<ObservablePoint>(
                    shipperAgg.Select(s => new ObservablePoint(s.AvgDays, s.OnTime))
                ),
                DataLabels = false
            };

            // We can’t attach labels per-point directly; show a legend-like hint:
            // Tip: Overlay a ListBox nearby, or add ToolTips:
            scatter.LabelPoint = p =>
            {
                var idx = (int)p.Key;
                // fallback: show coordinates; for named labels, use custom tooltip instead
                return $"{p.X:F1} d, {PercentFormatter(p.Y)}";
            };

            ShipperScatterSeries = new SeriesCollection { scatter };
        }

        private static int QuarterOf(DateTime dt) => (dt.Month - 1) / 3 + 1;
    }
}
