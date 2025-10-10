using Northwind.Data;
using Northwind.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace Northwind.Modules.Interfaces
{
    public interface IEmployeeViewModel : IEntityViewModel<Employee>
    {
        int EmployeeId { get; set; }
        string LastName { get; set; }
        string FirstName { get; set; }
        string Title { get; set; }
        string TitleOfCourtesy { get; set; }
        DateTime? BirthDate { get; set; }
        DateTime? HireDate { get; set; }
        string Address { get; set; }
        string City { get; set; }
        string Region { get; set; }
        string PostalCode { get; set; }
        string Country { get; set; }
        string HomePhone { get; set; }
        string Extension { get; set; }
        string Notes { get; set; }
        int? ReportsTo { get; set; }
        string FullName { get; }
        string FullAddress { get; }
        string FullNameWithTitle { get; }
        string FullNameWithTitleAndPosition { get; }
        string FullNameWithPositionAndTitle { get; }
        string Email { get; }
        string Phone { get; }
        byte[]? Photo { get; }
        IEmployeeViewModel ReportsToEmployee { get; set; }
        ObservableCollection<IEmployeeViewModel> ReportingEmployees { get; set; }
    }
}
