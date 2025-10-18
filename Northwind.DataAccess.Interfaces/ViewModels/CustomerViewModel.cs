using Northwind.Data;
using Northwind.Mvvm;
using Prism.Ioc;

namespace Northwind.ViewModels
{
    public class CustomerViewModel: EntityViewModel<Customer>
    {

        private string _customerId = string.Empty;
        private string _companyName = string.Empty;
        private string _contactName = string.Empty;
        private string _contactTitle = string.Empty;
        private string _address = string.Empty;
        private string _city = string.Empty;
        private string _region = string.Empty;
        private string _postalCode = string.Empty;
        private string _country = string.Empty;
        private string _phone = string.Empty;
        private string _fax = string.Empty;

        public string CustomerId
        {
            get => _customerId;
            set => SetProperty(ref _customerId, value);
        }
        public string CompanyName
        {
            get => _companyName;
            set => SetProperty(ref _companyName, value);
        }
        public string ContactName
        {
            get => _contactName;
            set => SetProperty(ref _contactName, value);
        }
        public string ContactTitle
        {
            get => _contactTitle;
            set => SetProperty(ref _contactTitle, value);
        }
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }
        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }
        public string Region
        {
            get => _region;
            set => SetProperty(ref _region, value);
        }
        public string PostalCode
        {
            get => _postalCode;
            set => SetProperty(ref _postalCode, value);
        }
        public string Country
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }
        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }
        public string Fax
        {
            get => _fax;
            set => SetProperty(ref _fax, value);
        }

        public string ContactNameAndTitle => $"{ContactTitle.Trim()} | {ContactName.Trim()}";
    }
}
