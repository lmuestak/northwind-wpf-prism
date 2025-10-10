using Northwind.Mvvm;

namespace Northwind.Data
{
    public class OrderViewModel(IContainerExtension container) : EntityViewModel<Order>(container)
    {
        private int _orderId;
        private string? _customerId;
        private int? _employeeId;
        private DateTime? _orderDate;
        private DateTime? _requiredDate;
        private DateTime? _shippedDate;
        private int? _shipVia;
        private decimal? _freight;
        private string? _shipName;
        private string? _shipAddress;
        private string? _shipCity;
        private string? _shipRegion;
        private string? _shipPostalCode;
        private string? _shipCountry;
        public int OrderId
        {
            get => _orderId;
            set => SetProperty(ref _orderId, value);
        }
        public string? CustomerId
        {
            get => _customerId;
            set => SetProperty(ref _customerId, value);
        }
        public int? EmployeeId
        {
            get => _employeeId;
            set => SetProperty(ref _employeeId, value);
        }
        public DateTime? OrderDate
        {
            get => _orderDate;
            set => SetProperty(ref _orderDate, value);
        }
        public DateTime? RequiredDate
        {
            get => _requiredDate;
            set => SetProperty(ref _requiredDate, value);
        }
        public DateTime? ShippedDate
        {
            get => _shippedDate;
            set => SetProperty(ref _shippedDate, value);
        }
        public int? ShipVia
        {
            get => _shipVia;
            set => SetProperty(ref _shipVia, value);
        }
        public decimal? Freight
        {
            get => _freight;
            set => SetProperty(ref _freight, value);
        }
        public string? ShipName
        {
            get => _shipName;
            set => SetProperty(ref _shipName, value);
        }
        public string? ShipAddress
        {
            get => _shipAddress;
            set => SetProperty(ref _shipAddress, value);
        }
        public string? ShipCity
        {
            get => _shipCity;
            set => SetProperty(ref _shipCity, value);
        }
        public string? ShipRegion
        {
            get => _shipRegion;
            set => SetProperty(ref _shipRegion, value);
        }
        public string? ShipPostalCode
        {
            get => _shipPostalCode;
            set => SetProperty(ref _shipPostalCode, value);
        }
        public string? ShipCountry
        {
            get => _shipCountry;
            set => SetProperty(ref _shipCountry, value);
        }

    }
}