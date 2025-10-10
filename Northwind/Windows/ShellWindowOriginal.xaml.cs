using System.Windows;
using System.Windows.Input;

namespace Northwind.Windows
{
    /// <summary>
    /// Interaction logic for ShellWindowOriginal.xaml
    /// </summary>
    public partial class ShellWindowOriginal : Window
    {
        public ShellWindowOriginal()
        {
            InitializeComponent();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
    }
}
