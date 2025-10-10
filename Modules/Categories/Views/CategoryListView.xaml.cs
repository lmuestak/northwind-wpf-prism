using Northwind.Core;
using System.Windows.Controls;

namespace Northwind.Modules.Views
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
