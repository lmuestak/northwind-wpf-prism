using Northwind.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Northwind.Controls
{
    /// <summary>
    /// Interaction logic for ProductSelector.xaml
    /// </summary>
    public partial class ProductSelector : UserControl
    {

        public static readonly DependencyProperty ProductProperty = DependencyProperty.Register("Product", typeof(ProductViewModel), typeof(ProductSelector), new PropertyMetadata(null));
        public static readonly DependencyProperty ProductsProperty = DependencyProperty.Register("Products", typeof(IEnumerable<ProductViewModel>), typeof(ProductSelector), new PropertyMetadata(null));

        public ProductViewModel Product
        {
            get { return (ProductViewModel)GetValue(ProductProperty); }
            set { SetValue(ProductProperty, value); }
        }

        public IEnumerable<ProductViewModel> Products
        {
            get { return (IEnumerable<ProductViewModel>)GetValue(ProductsProperty); }
            set { SetValue(ProductsProperty, value); }
        }

        public ProductSelector()
        {
            InitializeComponent();
        }
    }
}
