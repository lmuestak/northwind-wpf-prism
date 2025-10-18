using System.Windows.Controls;

namespace Northwind.Views
{
    /// <summary>
    /// Interaction logic for ProductDashboardView.xaml
    /// </summary>
    public partial class ProductDashboardView : UserControl, INamedView
    {
        public ProductDashboardView()
        {
            InitializeComponent();
        }

        public string ViewName => "Products Dashboard";
    }
}
