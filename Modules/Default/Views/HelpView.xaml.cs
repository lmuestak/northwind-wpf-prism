using System.Windows.Controls;

namespace Northwind.Views
{
    /// <summary>
    /// Interaction logic for HelpView.xaml
    /// </summary>
    public partial class HelpView : UserControl, INamedView
    {
        public HelpView()
        {
            InitializeComponent();
        }

        public string ViewName => "Help";
    }
}
