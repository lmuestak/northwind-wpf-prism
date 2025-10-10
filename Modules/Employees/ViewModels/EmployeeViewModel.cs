using Northwind.Data;
using Northwind.Modules.Interfaces;
using Northwind.Mvvm;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Northwind.Modules.ViewModels
{
    public class EmployeeViewModel : EntityViewModel<Employee>, IEmployeeViewModel
    {

        private Employee _model;
        private int _employeeId;
        private string _lastName = string.Empty;
        private string _firstName = string.Empty;
        private string _title = string.Empty;
        private string _titleOfCourtesy = string.Empty;
        private DateTime? _birthDate;
        private DateTime? _hireDate;
        private string _address = string.Empty;
        private string _city = string.Empty;
        private string _region = string.Empty;
        private string _postalCode = string.Empty;
        private string _country = string.Empty;
        private string _homePhone = string.Empty;
        private string _extension = string.Empty;
        private string _notes = string.Empty;
        private int? _reportsTo;
        private IEmployeeViewModel _reportsToEmployee;
        private ObservableCollection<IEmployeeViewModel> _reportingEmployees;

        public EmployeeViewModel(IContainerExtension container) : base(container)
        {
        }
        public int EmployeeId
        {
            get => _employeeId;
            set => SetProperty(ref _employeeId, value);
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                if (SetProperty(ref _lastName, value))
                {
                    RaisePropertyChanged(nameof(Email));
                    RaisePropertyChanged(nameof(FullName));
                    RaisePropertyChanged(nameof(FullNameWithTitle));
                    RaisePropertyChanged(nameof(FullNameWithTitleAndPosition));
                    RaisePropertyChanged(nameof(FullNameWithPositionAndTitle));
                }
            }
        }
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (SetProperty(ref _firstName, value))
                {
                    RaisePropertyChanged(nameof(FullName));
                    RaisePropertyChanged(nameof(FullNameWithTitle));
                    RaisePropertyChanged(nameof(FullNameWithTitleAndPosition));
                    RaisePropertyChanged(nameof(FullNameWithPositionAndTitle));
                }
            }
        }
        public string Title
        {
            get => _title;
            set
            {
                if (SetProperty(ref _title, value))
                {
                    RaisePropertyChanged(nameof(FullName));
                    RaisePropertyChanged(nameof(FullNameWithTitle));
                    RaisePropertyChanged(nameof(FullNameWithTitleAndPosition));
                    RaisePropertyChanged(nameof(FullNameWithPositionAndTitle));
                }
            }
        }
        public string TitleOfCourtesy
        {
            get => _titleOfCourtesy;
            set
            {
                if (SetProperty(ref _titleOfCourtesy, value))
                {
                    RaisePropertyChanged(nameof(FullName));
                    RaisePropertyChanged(nameof(FullNameWithTitle));
                    RaisePropertyChanged(nameof(FullNameWithTitleAndPosition));
                    RaisePropertyChanged(nameof(FullNameWithPositionAndTitle));
                }
            }
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
        public string Address
        {
            get => _address;
            set
            {
                if (SetProperty(ref _address, value))
                {
                    RaisePropertyChanged(nameof(FullAddress));
                }
            }
        }
        public string City
        {
            get => _city;
            set
            {
                if (SetProperty(ref _city, value))
                {
                    RaisePropertyChanged(nameof(FullAddress));
                }
            }
        }
        public string Region
        {
            get => _region;
            set
            {
                if (SetProperty(ref _region, value))
                {
                    RaisePropertyChanged(nameof(FullAddress));
                }
            }
        }
        public string PostalCode
        {
            get => _postalCode;
            set
            {
                if (SetProperty(ref _postalCode, value))
                {
                    RaisePropertyChanged(nameof(FullAddress));
                }
            }
        }
        public string Country
        {
            get => _country;
            set
            {
                if (SetProperty(ref _country, value))
                {
                    RaisePropertyChanged(nameof(FullAddress));
                }
            }
        }
        public string HomePhone
        {
            get => _homePhone;
            set
            {
                if (SetProperty(ref _homePhone, value))
                {
                    RaisePropertyChanged(nameof(Phone));
                }
            }
        }
        public string Extension
        {
            get => _extension;
            set
            {
                if (SetProperty(ref _extension, value))
                {
                    RaisePropertyChanged(nameof(Phone));
                }
            }
        }
        public string Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }
        public int? ReportsTo
        {
            get => _reportsTo;
            set => SetProperty(ref _reportsTo, value);
        }
        public IEmployeeViewModel ReportsToEmployee
        {
            get => _reportsToEmployee;
            set => SetProperty(ref _reportsToEmployee, value);
        }
        public ObservableCollection<IEmployeeViewModel> ReportingEmployees
        {
            get => _reportingEmployees;
            set => SetProperty(ref _reportingEmployees, value);
        }

        public string FullName
        {
            get => $"{FirstName} {LastName}";
        }
        public string FullAddress
        {
            get
            {
                var addressParts = new List<string>
                {
                    Address,
                    City,
                    Region,
                    PostalCode,
                    Country
                };
                return string.Join(", ", addressParts.Where(part => !string.IsNullOrEmpty(part)));
            }
        }

        public string FullNameWithTitle
        {
            get => $"{TitleOfCourtesy} {FirstName} {LastName}";
        }
        public string FullNameWithTitleAndPosition
        {
            get => $"{TitleOfCourtesy} {FirstName} {LastName}, {Title}";
        }

        public string FullNameWithPositionAndTitle
        {
            get => $"{Title}, {TitleOfCourtesy} {FirstName} {LastName}";
        }

        public string Email => $"{LastName.ToLowerInvariant().Trim()}@northwind.com";

        public string Phone
        {
            get
            {
                var phone = $"{HomePhone.ToLowerInvariant().Trim()}{Extension.ToLowerInvariant().Trim()}";
                return !string.IsNullOrWhiteSpace(phone) ? phone : "N/A";
            }
        }

        public byte[]? Photo => Model?.Photo;
        public override Employee Model
        {
            get => _model;
            set
            {
                if (SetProperty(ref _model, value) && value != null)
                {
                    EmployeeId = value.EmployeeId;
                    LastName = value.LastName;
                    FirstName = value.FirstName;
                    Title = value.Title;
                    TitleOfCourtesy = value.TitleOfCourtesy;
                    BirthDate = value.BirthDate;
                    HireDate = value.HireDate;
                    Address = value.Address;
                    City = value.City;
                    Region = value.Region;
                    PostalCode = value.PostalCode;
                    Country = value.Country;
                    HomePhone = value.HomePhone;
                    Extension = value.Extension;
                    Notes = value.Notes;
                    ReportsTo = value.ReportsTo;
                }
            }
        }
    }
}
