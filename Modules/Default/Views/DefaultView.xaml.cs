using System.Windows.Controls;

namespace Northwind.Views
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
