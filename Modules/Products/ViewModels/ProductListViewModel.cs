using Prism.Mvvm;

namespace Northwind.ViewModels
{
    public class ProductListViewModel : BindableBase
    {
        private string _message = "ProductListView";
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ProductListViewModel()
        {
            //Message = "View A from your Prism Module";
        }
    }
}
