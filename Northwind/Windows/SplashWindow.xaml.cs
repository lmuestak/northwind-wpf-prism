using Northwind.ViewModels;
using System.Windows;

namespace Northwind.Windows
{
    /// <summary>
    /// Interaction logic for SplashWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window
    {
        public SplashWindow()
        {
            InitializeComponent();
        }

        public void SetStatus(string status, int value)
        {
            if (ViewModel != null)
            {
                ViewModel.Status = status;
                ViewModel.Value = value;
            }
        }

        public void SetMaximum(int maximum)
        {
            if (ViewModel != null)
            {
                ViewModel.Maximum = maximum;
            }
        }

        public SplashWindowViewModel ViewModel => DataContext as SplashWindowViewModel;

    }
}
