using Northwind.ViewModels;
using System.Windows;

namespace Northwind.Windows
{
    /// <summary>
    /// Interaction logic for SplashWindowOriginal.xaml
    /// </summary>
    public partial class SplashWindowOriginal : Window
    {
        public SplashWindowOriginal()
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
