using Northwind.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Northwind.Controls
{
    /// <summary>
    /// Interaction logic for ShipperSelector.xaml
    /// </summary>
    public partial class ShipperSelector : UserControl
    {

        public static readonly DependencyProperty ShipperProperty = DependencyProperty.Register("Shipper", typeof(ShipperViewModel), typeof(ShipperSelector), new PropertyMetadata(null));
        public static readonly DependencyProperty ShippersProperty = DependencyProperty.Register("Shippers", typeof(IEnumerable<ShipperViewModel>), typeof(ShipperSelector), new PropertyMetadata(null));

        public ShipperViewModel Shipper
        {
            get { return (ShipperViewModel)GetValue(ShipperProperty); }
            set { SetValue(ShipperProperty, value); }
        }

        public IEnumerable<ShipperViewModel> Shippers
        {
            get { return (IEnumerable<ShipperViewModel>)GetValue(ShippersProperty); }
            set { SetValue(ShippersProperty, value); }
        }

        public ShipperSelector()
        {
            InitializeComponent();
        }
    }
}
