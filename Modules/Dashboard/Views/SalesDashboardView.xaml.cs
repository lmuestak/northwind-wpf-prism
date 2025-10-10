using Northwind.Core;
using System.Windows.Controls;

namespace Northwind.Modules.Views
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
