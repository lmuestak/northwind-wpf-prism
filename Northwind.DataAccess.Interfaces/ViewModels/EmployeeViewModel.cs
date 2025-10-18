using Northwind.Data;
using Northwind.Mvvm;

namespace Northwind.ViewModels
{
    public class EmployeeViewModel : EntityViewModel<Employee>
    {

        private int _employeeId;
        private string _lastName = string.Empty;
        private string _firstName = string.Empty;
        private string _title = string.Empty;
        private string _titleOfCourtesy = string.Empty;
        private DateTime? _birthDate;
        private DateTime? _hireDate;
        private string? _address = string.Empty;
        private string? _city = string.Empty;
        private string? _region = string.Empty;
        private string? _postalCode = string.Empty;
        private string? _country = string.Empty;
        private string? _homePhone = string.Empty;
        private string? _extension = string.Empty;
        private byte[]? _photo;
        private string? _notes = string.Empty;
        private int? _reportsTo;
        private string? _photoPath = string.Empty;
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
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        public string TitleOfCourtesy
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
        public string Email => $"{LastName.ToLower()}@northwind.com";
        public string FullName => $"{LastName}, {FirstName}";
        public string TitleOfCourtesyAndPosition => $"{TitleOfCourtesy} | {Title}";
        public string FullAddress => $"{Address}, {City}, {Region}, {PostalCode}, {Country}";
        public string Phone => $"Phone: {HomePhone}, Ext: {Extension}";

    }
}
