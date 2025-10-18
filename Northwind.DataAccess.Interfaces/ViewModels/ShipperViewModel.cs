using Northwind.Data;
using Northwind.Mvvm;

namespace Northwind.ViewModels
{
    public class ShipperViewModel : EntityViewModel<Shipper>
    {
        private int? _shipperId;
        private string? _companyName = string.Empty;
        private string? _phone = string.Empty;
        public int? ShipperId
        {
            get => _shipperId;
            set => SetProperty(ref _shipperId, value);
        }
        public string? CompanyName
        {
            get => _companyName;
            set => SetProperty(ref _companyName, value);
        }
        public string? Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }
        public string? CompanyAndPhone
        {
            get => $"{CompanyName?.Trim()} ({Phone})";
        }
    }
}
