using Northwind.Core;
using System.Windows.Controls;

namespace Northwind.Modules.Views
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
