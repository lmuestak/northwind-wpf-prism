using System.Windows.Controls;

namespace Northwind.Views
{
    /// <summary>
    /// Interaction logic for ProductListView.xaml
    /// </summary>
    public partial class ProductListView : UserControl, INamedView
    {
        public ProductListView()
        {
            InitializeComponent();
        }

        public string ViewName => "Products";
    }
}
