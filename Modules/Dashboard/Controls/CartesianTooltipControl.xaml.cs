using LiveCharts;
using LiveCharts.Wpf;
using System.ComponentModel;
using System.Windows.Controls;

namespace Northwind.Modules.Controls
{
    /// <summary>
    /// Interaction logic for CartesianTooltipControl.xaml
    /// </summary>
    public partial class CartesianTooltipControl : UserControl, IChartTooltip
    {

        private TooltipData _data;
        public CartesianTooltipControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TooltipData Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged(nameof(Data));
            }
        }

        public TooltipSelectionMode? SelectionMode { get; set; }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
