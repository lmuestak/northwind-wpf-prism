using Northwind.Core;
using System.Windows.Controls;

namespace Northwind.Modules.Default.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class DefaultView : UserControl, INamedView
    {
        public DefaultView()
        {
            InitializeComponent();
        }

        public string ViewName => "Home";
    }
}
