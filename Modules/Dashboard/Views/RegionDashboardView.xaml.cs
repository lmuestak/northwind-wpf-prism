using System.Windows.Controls;

namespace Northwind.Views
{
    /// <summary>
    /// Interaction logic for RegionDashboardView.xaml
    /// </summary>
    public partial class RegionDashboardView : UserControl, INamedView
    {
        public RegionDashboardView()
        {
            InitializeComponent();
        }

        public string ViewName => "Regions Dashboard";
    }
}
