using LiveCharts;
using Northwind.Data;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.ViewModels.Charts
{
    public abstract class CartesianChartViewModelBase: BindableBase
    {

        private int _column = 0;
        private int _row = 0;
        private int _rowSpan = 0;
        private int _columnSpan = 0;
        private string _title = string.Empty;
        private string[] _labels = [];
        private SeriesCollection _series = [];
        private bool _showLabels;

        public CartesianChartViewModelBase(string title)
        {
            Title = title;
        }

        public string Title
        {
            get { return _title; }
            protected set { SetProperty(ref _title, value); }
        }

        public bool ShowLabels
        {
            get => _showLabels;
            protected set => SetProperty(ref _showLabels, value);
        }

        public int Row
        {
            get => _row;
            protected set => SetProperty(ref _row, value);
        }

        public int RowSpan
        {
            get => _rowSpan;
            protected set => SetProperty(ref _rowSpan, value);
        }

        public int Column
        {
            get => _column;
            protected set => SetProperty(ref _column, value);
        }

        public int ColumnSpan
        {
            get => _columnSpan;
            protected set => SetProperty(ref _columnSpan, value);
        }

        public SeriesCollection Series
        {
            get { return _series ??= []; }
            protected set { SetProperty(ref _series, value); }
        }

        public string[] Labels
        {
            get { return _labels; }
            protected set { SetProperty(ref _labels, value); }
        }

        public double AxisXMin { get; protected set; }
        public double AxisXMax { get; protected set; }
        public double AxisStep { get; protected set; } // ticks between labels (≈ 1 month)
        public Func<double, string> XFormatter { get; protected set; }
        public Func<double, string> YFormatter { get; protected set; }

        public abstract Task Transform(IEnumerable<SalesReportData> data);

    }
}
