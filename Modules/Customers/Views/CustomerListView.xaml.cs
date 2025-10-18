using System.Windows.Controls;

namespace Northwind.Views
{
    /// <summary>
    /// Interaction logic for CustomerListView.xaml
    /// </summary>
    public partial class CustomerListView : UserControl, INamedView
    {
        public CustomerListView()
        {
            InitializeComponent();
        }

        public string ViewName => "Customers";
    }
}
