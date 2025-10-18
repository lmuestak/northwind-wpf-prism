using LiveCharts;
using LiveCharts.Wpf;
using Northwind.DataAccess;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Northwind.ViewModels.Charts
{
    /*
    public class PieChartViewModel : BindableBase
    {
        private int _column = 0;
        private int _row = 0;
        private int _rowSpan = 0;
        private int _columnSpan = 0;
        private string _title = string.Empty;
        private string[] _labels;
        private SeriesCollection _series;
        private bool _showLabels;
        public PieChartViewModel(string title, IEnumerable<SalesOverviewByCategory> sales)
        {
            Title = title;
            CreateSeries(sales);
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
            var grouped = sales
                        .GroupBy(s => s.CategoryName)
                        .Select(g => new
                        {
                            Category = g.Key,
                            TotalSales = g.Sum(x => x.SalesPrice)
                        })
                        .ToList();

            // Create pie series collection
            var series = new SeriesCollection();
            foreach (var item in grouped)
            {
                series.Add(new PieSeries
                {
                    Title = item.Category,
                    Values = new ChartValues<decimal> { item.TotalSales },
                    DataLabels = true,
                    LabelPoint = cp => string.Format(CultureInfo.CurrentCulture, "{0:C2}", cp.Y),
                    LabelPosition = PieLabelPosition.InsideSlice
                });
            }
            Series = series;
        }

    }
    */
}
