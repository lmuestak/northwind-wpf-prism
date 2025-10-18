using Northwind.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Northwind.Controls
{
    /// <summary>
    /// Interaction logic for TerritorySelector.xaml
    /// </summary>
    public partial class TerritorySelector : UserControl
    {

        public static readonly DependencyProperty TerritoryProperty = DependencyProperty.Register("Territory", typeof(TerritoryViewModel), typeof(TerritorySelector), new PropertyMetadata(null));
        public static readonly DependencyProperty TerritoriesProperty = DependencyProperty.Register("Territories", typeof(IEnumerable<TerritoryViewModel>), typeof(TerritorySelector), new PropertyMetadata(null));

        public TerritoryViewModel Territory
        {
            get { return (TerritoryViewModel)GetValue(TerritoryProperty); }
            set { SetValue(TerritoryProperty, value); }
        }

        public IEnumerable<TerritoryViewModel> Territories
        {
            get { return (IEnumerable<TerritoryViewModel>)GetValue(TerritoriesProperty); }
            set { SetValue(TerritoriesProperty, value); }
        }

        public TerritorySelector()
        {
            InitializeComponent();
        }
    }
}
