using System.Windows.Controls;

namespace Northwind.Views
{
    /// <summary>
    /// Interaction logic for CategoryListView.xaml
    /// </summary>
    public partial class CategoryListView : UserControl, INamedView
    {
        public CategoryListView()
        {
            InitializeComponent();
        }

        public string ViewName => "Categories";
    }
}
