using Northwind.Mvvm;

namespace Northwind.Data
{
    public class EmployeeViewModel(IContainerExtension container) : EntityViewModel<Employee>(container)
    {

        private int _employeeId;
        private string _lastName = string.Empty;
        private string _firstName = string.Empty;
        private string? _title;
        private string? _titleOfCourtesy;
        private DateTime? _birthDate;
        private DateTime? _hireDate;
        private string? _address;
        private string? _city;
        private string? _region;
        private string? _postalCode;
        private string? _country;
        private string? _homePhone;
        private string? _extension;
        private byte[]? _photo;
        private string? _notes;
        private int? _reportsTo;
        private string? _photoPath;
        public int EmployeeId
        {
            get => _employeeId;
            set => SetProperty(ref _employeeId, value);
        }
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }
        public string? Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        public string? TitleOfCourtesy
        {
            get => _titleOfCourtesy;
            set => SetProperty(ref _titleOfCourtesy, value);
        }
        public DateTime? BirthDate
        {
            get => _birthDate;
            set => SetProperty(ref _birthDate, value);
        }
        public DateTime? HireDate
        {
            get => _hireDate;
            set => SetProperty(ref _hireDate, value);
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
        public string? HomePhone
        {
            get => _homePhone;
            set => SetProperty(ref _homePhone, value);
        }
        public string? Extension
        {
            get => _extension;
            set => SetProperty(ref _extension, value);
        }
        public byte[]? Photo
        {
            get => _photo;
            set => SetProperty(ref _photo, value);
        }
        public string? Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }
        public int? ReportsTo
        {
            get => _reportsTo;
            set => SetProperty(ref _reportsTo, value);
        }
        public string? PhotoPath
        {
            get => _photoPath;
            set => SetProperty(ref _photoPath, value);
        }
    }
}
