using System.Windows.Controls;

namespace Northwind.Views
{
    /// <summary>
    /// Interaction logic for EmployeeDashboardView.xaml
    /// </summary>
    public partial class EmployeeDashboardView : UserControl, INamedView
    {
        public EmployeeDashboardView()
        {
            InitializeComponent();
        }

        public string ViewName => "Employees Dashboards";
    }
}
