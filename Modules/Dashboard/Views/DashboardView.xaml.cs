using System.Windows.Controls;

namespace Northwind.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class DashboardView : UserControl, INamedView
    {
        public DashboardView()
        {
            InitializeComponent();
        }

        public string ViewName => "Dashboard overview";
    }
}
