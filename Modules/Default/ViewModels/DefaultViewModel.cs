using Northwind.Mvvm;
using Northwind.Services.Interfaces;
using Prism.Ioc;
using Prism.Navigation.Regions;

namespace Northwind.ViewModels
{
    public class DefaultViewModel : ViewModelBase
    {
        private string _message = "Default View";
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        //public DefaultViewModel(IContainerExtension container, IMessageService messageService) :
        //    base(container)
        //{
        //    //Message = messageService.GetMessage();
        //}

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //do something
        }
    }
}
