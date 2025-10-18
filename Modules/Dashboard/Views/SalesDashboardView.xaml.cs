using System.Windows.Controls;

namespace Northwind.Views
{
    /// <summary>
    /// Interaction logic for SalesView.xaml
    /// </summary>
    public partial class SalesDashboardView : UserControl, INamedView
    {
        public SalesDashboardView()
        {
            InitializeComponent();
        }

        public string ViewName => "Sales Dashboard";
    }
}
