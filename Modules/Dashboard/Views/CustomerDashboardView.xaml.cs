using System.Windows.Controls;

namespace Northwind.Views
{
    /// <summary>
    /// Interaction logic for CustomerDashboardView.xaml
    /// </summary>
    public partial class CustomerDashboardView : UserControl, INamedView
    {
        public CustomerDashboardView()
        {
            InitializeComponent();
        }

        public string ViewName => "Customers Dashboard";
    }
}
