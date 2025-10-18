using Northwind.Mvvm;
using Prism.Ioc;

namespace Northwind.ViewModels
{
    public class SplashWindowViewModel : ViewModelBase
    {
        private int _maximum = 100;
        private int _value = 0;
        private string _status = "Initializing...";

        //public SplashWindowViewModel(IContainerExtension container) : base(container)
        //{
        //}

        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }
        public int Maximum
        {
            get => _maximum;
            set => SetProperty(ref _maximum, value);
        }
        public int Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
    }
}
