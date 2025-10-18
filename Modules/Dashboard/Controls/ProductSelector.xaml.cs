using Northwind.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Northwind.Controls
{
    /// <summary>
    /// Interaction logic for CategorySelector.xaml
    /// </summary>
    public partial class CategorySelector : UserControl
    {

        public static readonly DependencyProperty CategoryProperty = DependencyProperty.Register("Category", typeof(CategoryViewModel), typeof(CategorySelector), new PropertyMetadata(null));
        public static readonly DependencyProperty CategoriesProperty = DependencyProperty.Register("Categories", typeof(IEnumerable<CategoryViewModel>), typeof(CategorySelector), new PropertyMetadata(null));

        public CategoryViewModel Category
        {
            get { return (CategoryViewModel)GetValue(CategoryProperty); }
            set { SetValue(CategoryProperty, value); }
        }

        public IEnumerable<CategoryViewModel> Categories
        {
            get { return (IEnumerable<CategoryViewModel>)GetValue(CategoriesProperty); }
            set { SetValue(CategoriesProperty, value); }
        }

        public CategorySelector()
        {
            InitializeComponent();
        }
    }
}
