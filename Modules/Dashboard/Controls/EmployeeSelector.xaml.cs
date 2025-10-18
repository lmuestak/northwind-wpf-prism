using Northwind.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Northwind.Controls
{
    /// <summary>
    /// Interaction logic for EmployeeSelector.xaml
    /// </summary>
    public partial class EmployeeSelector : UserControl
    {

        public static readonly DependencyProperty EmployeeProperty = DependencyProperty.Register("Employee", typeof(EmployeeViewModel), typeof(EmployeeSelector), new PropertyMetadata(null));
        public static readonly DependencyProperty EmployeesProperty = DependencyProperty.Register("Employees", typeof(IEnumerable<EmployeeViewModel>), typeof(EmployeeSelector), new PropertyMetadata(null));

        public EmployeeViewModel Employee
        {
            get { return (EmployeeViewModel)GetValue(EmployeeProperty); }
            set { SetValue(EmployeeProperty, value); }
        }

        public IEnumerable<EmployeeViewModel> Employees
        {
            get { return (IEnumerable<EmployeeViewModel>)GetValue(EmployeesProperty); }
            set { SetValue(EmployeesProperty, value); }
        }

        public EmployeeSelector()
        {
            InitializeComponent();
        }
    }
}
