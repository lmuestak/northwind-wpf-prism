using Northwind.Core;
using System.Windows.Controls;

namespace Northwind.Modules.Views
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
