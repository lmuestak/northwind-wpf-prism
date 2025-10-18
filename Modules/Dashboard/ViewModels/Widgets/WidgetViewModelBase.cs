using LiveCharts;
using Northwind.Mvvm;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.ViewModels.Widgets
{
    public abstract class WidgetViewModelBase<T> : ViewModelBase
    {

        private string _title;
        private SeriesCollection _series;
        private string[] _labels;
        private double _innerRadius = 0;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public SeriesCollection Series
        {
            get => _series;
            set => SetProperty(ref _series, value);
        }

        public string[] Labels
        {
            get => _labels;
            set => SetProperty(ref _labels, value);
        }

        public double InnerRadius
        {
            get => _innerRadius;
            set => SetProperty(ref _innerRadius, value);
        }

        public abstract Task PrepareData(IList<T> data, CancellationToken cancellationToken = default);
    }
}
