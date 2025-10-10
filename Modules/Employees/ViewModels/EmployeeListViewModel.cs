using Northwind.Mvvm;
using Prism.Ioc;

namespace Northwind.Modules.ViewModels
{
    public class EmployeeListViewModel : ViewModelBase
    {
        public EmployeeListViewModel(IContainerExtension container) : base(container)
        {
        }
    }
}
