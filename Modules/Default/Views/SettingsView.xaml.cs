using System.Windows.Controls;

namespace Northwind.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl, INamedView
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        public string ViewName => "Settings";
    }
}
