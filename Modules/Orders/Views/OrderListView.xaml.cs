using System.Windows.Controls;

namespace Northwind.Views
{
    /// <summary>
    /// Interaction logic for OrderListView.xaml
    /// </summary>
    public partial class OrderListView : UserControl, INamedView
    {
        public OrderListView()
        {
            InitializeComponent();
        }

        public string ViewName => "Orders";
    }
}
