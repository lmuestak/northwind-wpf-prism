using Northwind.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Northwind.Controls
{
    /// <summary>
    /// Interaction logic for CustomerSelector.xaml
    /// </summary>
    public partial class CustomerSelector : UserControl
    {

        public static readonly DependencyProperty CustomerProperty = DependencyProperty.Register("Customer", typeof(CustomerViewModel), typeof(CustomerSelector), new PropertyMetadata(null));
        public static readonly DependencyProperty CustomersProperty = DependencyProperty.Register("Customers", typeof(IEnumerable<CustomerViewModel>), typeof(CustomerSelector), new PropertyMetadata(null));

        public CustomerViewModel Customer
        {
            get { return (CustomerViewModel)GetValue(CustomerProperty); }
            set { SetValue(CustomerProperty, value); }
        }

        public IEnumerable<CustomerViewModel> Customers
        {
            get { return (IEnumerable<CustomerViewModel>)GetValue(CustomersProperty); }
            set { SetValue(CustomersProperty, value); }
        }

        public CustomerSelector()
        {
            InitializeComponent();
        }
    }
}
