using System.Windows;
using System.Windows.Input;

namespace Northwind.Windows
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : Window
    {
        public ShellWindow()
        {
            InitializeComponent();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void NavigationControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
