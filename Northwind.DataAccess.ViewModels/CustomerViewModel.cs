using Northwind.Mvvm;

namespace Northwind.Data
{
    public class CustomerViewModel(IContainerExtension container) : EntityViewModel<Customer>(container)
    {

        private string _customerId = string.Empty;
        private string _companyName = string.Empty;
        private string? _contactName;
        private string? _contactTitle;
        private string? _address;
        private string? _city;
        private string? _region;
        private string? _postalCode;
        private string? _country;
        private string? _phone;
        private string? _fax;

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
        public string? ContactName
        {
            get => _contactName;
            set => SetProperty(ref _contactName, value);
        }
        public string? ContactTitle
        {
            get => _contactTitle;
            set => SetProperty(ref _contactTitle, value);
        }
        public string? Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }
        public string? City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }
        public string? Region
        {
            get => _region;
            set => SetProperty(ref _region, value);
        }
        public string? PostalCode
        {
            get => _postalCode;
            set => SetProperty(ref _postalCode, value);
        }
        public string? Country
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }
        public string? Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }
        public string? Fax
        {
            get => _fax;
            set => SetProperty(ref _fax, value);
        }
    }
}
