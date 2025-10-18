using Northwind.Mvvm;
using Northwind.Services.Interfaces;
using Prism.Ioc;
using Prism.Navigation.Regions;

namespace Northwind.ViewModels
{
    public class HelpViewModel : ViewModelBase
    {
        private string _message = "Help View";
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        //public HelpViewModel(IContainerExtension container, IMessageService messageService) :
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
