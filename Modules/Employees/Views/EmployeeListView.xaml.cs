using System.Windows.Controls;

namespace Northwind.Views
{
    /// <summary>
    /// Interaction logic for EmployeeListView.xaml
    /// </summary>
    public partial class EmployeeListView : UserControl, INamedView
    {
        public EmployeeListView()
        {
            InitializeComponent();
        }

        public string ViewName => "Employees";
    }
}
