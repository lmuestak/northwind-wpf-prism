using Northwind.Core;
using System.Windows.Controls;

namespace Northwind.Modules.Views
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
