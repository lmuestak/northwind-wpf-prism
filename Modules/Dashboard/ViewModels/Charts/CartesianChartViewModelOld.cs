using LiveCharts;
using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace Northwind.ViewModels.Charts
{
    public class CartesianChartViewModelOld : BindableBase
    {
        /*
        private int _column = 0;
        private int _row = 0;
        private int _rowSpan = 0;
        private int _columnSpan = 0;
        private string _title = string.Empty;
        private string[] _labels;
        private SeriesCollection _series;
        private bool _showLabels;
        public CartesianChartViewModelOld(string title, IEnumerable<SalesOverviewByCategory> sales)
        {
            Title = title;
            CreateSeries2(sales);
        }

        public double AxisXMin { get; private set; }
        public double AxisXMax { get; private set; }
        public double AxisStep { get; private set; } // ticks between labels (≈ 1 month)
        public Func<double, string> XFormatter { get; private set; }
        public Func<double, string> YFormatter { get; private set; }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public bool ShowLabels
        {
            get => _showLabels;
            set => SetProperty(ref _showLabels, value);
        }

        public int Row
        {
            get => _row;
            set => SetProperty(ref _row, value);
        }

        public int RowSpan
        {
            get => _rowSpan;
            set => SetProperty(ref _rowSpan, value);
        }

        public int Column
        {
            get => _column;
            set => SetProperty(ref _column, value);
        }

        public int ColumnSpan
        {
            get => _columnSpan;
            set => SetProperty(ref _columnSpan, value);
        }

        public SeriesCollection Series
        {
            get { return _series ??= []; }
            set { SetProperty(ref _series, value); }
        }

        public string[] Labels
        {
            get { return _labels; }
            set { SetProperty(ref _labels, value); }
        }

        public Func<ChartPoint, string> TooltipLabelFormatter => (ChartPoint chartPoint) => $"{chartPoint.Sum}";

        private void CreateSeries(IEnumerable<SalesOverviewByCategory> sales)
        {
            // 1) Build a stable list of all yyyy-MM buckets present
            var monthKeys = sales
                .Select(r => r.OrderDate.ToString("yyyy-MM"))
                .Distinct()
                .OrderBy(s => s)
                .ToList();

            Labels = [.. monthKeys];

            XFormatter = val => new DateTime((long)val).ToString("yyyy-MM");
            YFormatter = val => val.ToString("C0", CultureInfo.CurrentCulture);

            // 2) Group by Category, then map each month to sum of SalesPrice
            var series = new SeriesCollection();

            var byCategory = sales
                .GroupBy(r => r.CategoryName)
                .OrderBy(g => g.Key);

            foreach (var cat in byCategory)
            {
                // Precompute sums per month for this category
                var sumsByMonth = cat
                    .GroupBy(r => r.OrderDate.ToString("yyyy-MM"))
                    .ToDictionary(g => g.Key, g => g.Sum(x => (double)x.SalesPrice));

                var values = new ChartValues<double>();
                foreach (var mk in monthKeys)
                {
                    values.Add(sumsByMonth.TryGetValue(mk, out var v) ? v : 0d);
                }

                series.Add(new LineSeries
                {
                    Title = cat.Key,
                    Values = values,
                    PointGeometry = DefaultGeometries.Circle,
                    StrokeThickness = 2,
                    Fill = System.Windows.Media.Brushes.Transparent // cleaner lines
                });
            }

            Series = series;
        }
        private void CreateSeries2(IEnumerable<SalesOverviewByCategory> sales)
        {
            // Map DateTimePoint.DateTime -> Ticks for X, Value -> Y
            var mapper = Mappers.Xy<DateTimePoint>()
                .X(dp => dp.DateTime.Ticks)
                .Y(dp => dp.Value);

            Charting.For<DateTimePoint>(mapper);

            // Build month buckets (first day of each yyyy-MM)
            var months = sales
                .Select(r => new DateTime(r.OrderDate.Year, r.OrderDate.Month, 1))
                .Distinct()
                .OrderBy(d => d)
                .ToList();

            if (months.Count == 0)
            {
                // Empty-safe defaults
                AxisXMin = DateTime.Today.AddMonths(-1).Ticks;
                AxisXMax = DateTime.Today.Ticks;
            }
            else
            {
                AxisXMin = months.First().Ticks;
                // include the last month fully
                AxisXMax = months.Last().AddMonths(1).AddTicks(-1).Ticks;
            }

            // ~monthly tick step (30 days in ticks is fine for labeling yyyy-MM)
            AxisStep = TimeSpan.FromDays(30).Ticks;

            XFormatter = val => new DateTime((long)val).ToString("yyyy-MM");
            YFormatter = val => val.ToString("C2", CultureInfo.CurrentCulture);

            // Group by Category, then aggregate by yyyy-MM (sum SalesPrice)
            var byCategory = sales
                .GroupBy(r => r.CategoryName)
                .OrderBy(g => g.Key);

            var series = new SeriesCollection();

            foreach (var cat in byCategory)
            {
                var points = cat
                    .GroupBy(r => new DateTime(r.OrderDate.Year, r.OrderDate.Month, 1))
                    .Select(g => new DateTimePoint(g.Key, (double)g.Sum(x => x.SalesPrice)))
                    .OrderBy(p => p.DateTime)
                    .ToList();

                // If you want continuous lines across missing months, uncomment this block to inject zeros:
                points = FillMissingMonthsWithZero(points, months);

                series.Add(new LineSeries
                {
                    Title = cat.Key,
                    Values = new ChartValues<DateTimePoint>(points),
                    PointGeometry = DefaultGeometries.Circle,
                    //StrokeThickness = 2,
                    //Fill = System.Windows.Media.Brushes.Transparent
                });
            }

            Series = series;
        }

        // Optional helper to ensure every category has a point each month
        private static List<DateTimePoint> FillMissingMonthsWithZero(
            List<DateTimePoint> existing, List<DateTime> allMonths)
        {
            var dict = existing.ToDictionary(p => p.DateTime, p => p.Value);
            var filled = new List<DateTimePoint>(allMonths.Count);
            foreach (var m in allMonths)
            {
                if (!dict.TryGetValue(m, out var v)) v = 0d;
                filled.Add(new DateTimePoint(m, v));
            }
            return filled;
        }
        */
    }

}
