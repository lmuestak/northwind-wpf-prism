using Northwind.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Northwind.Controls
{
    /// <summary>
    /// Interaction logic for SupplierSelector.xaml
    /// </summary>
    public partial class SupplierSelector : UserControl
    {

        public static readonly DependencyProperty SupplierProperty = DependencyProperty.Register("Supplier", typeof(SupplierViewModel), typeof(SupplierSelector), new PropertyMetadata(null));
        public static readonly DependencyProperty SuppliersProperty = DependencyProperty.Register("Suppliers", typeof(IEnumerable<SupplierViewModel>), typeof(SupplierSelector), new PropertyMetadata(null));

        public SupplierViewModel Supplier
        {
            get { return (SupplierViewModel)GetValue(SupplierProperty); }
            set { SetValue(SupplierProperty, value); }
        }

        public IEnumerable<SupplierViewModel> Suppliers
        {
            get { return (IEnumerable<SupplierViewModel>)GetValue(SuppliersProperty); }
            set { SetValue(SuppliersProperty, value); }
        }

        public SupplierSelector()
        {
            InitializeComponent();
        }
    }
}
