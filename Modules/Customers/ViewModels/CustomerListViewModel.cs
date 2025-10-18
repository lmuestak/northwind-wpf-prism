using Prism.Mvvm;

namespace Northwind.ViewModels
{
    public class CustomerListViewModel : BindableBase
    {
        private string _message = "CustomerListView";
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public CustomerListViewModel()
        {
            //Message = "View A from your Prism Module";
        }
    }
}
