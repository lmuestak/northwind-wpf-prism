using Northwind.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Northwind.Controls
{
    /// <summary>
    /// Interaction logic for RegionSelector.xaml
    /// </summary>
    public partial class RegionSelector : UserControl
    {

        public static readonly DependencyProperty RegionProperty = DependencyProperty.Register("Region", typeof(RegionViewModel), typeof(RegionSelector), new PropertyMetadata(null));
        public static readonly DependencyProperty RegionsProperty = DependencyProperty.Register("Regions", typeof(IEnumerable<RegionViewModel>), typeof(RegionSelector), new PropertyMetadata(null));

        public RegionViewModel Region
        {
            get { return (RegionViewModel)GetValue(RegionProperty); }
            set { SetValue(RegionProperty, value); }
        }

        public IEnumerable<RegionViewModel> Regions
        {
            get { return (IEnumerable<RegionViewModel>)GetValue(RegionsProperty); }
            set { SetValue(RegionsProperty, value); }
        }

        public RegionSelector()
        {
            InitializeComponent();
        }
    }
}
