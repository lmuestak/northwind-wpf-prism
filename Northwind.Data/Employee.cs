using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data
{
    [Table("Employees", Schema = "dbo")]
    public partial class Employee
    {
        /// <summary>
        /// Gets or sets the employee id.
        /// </summary>
        [Key]
        [Column("EmployeeID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [Required]
        [StringLength(20)]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [Required]
        [StringLength(10)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [StringLength(30)]
        public string? Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the title of courtesy.
        /// </summary>
        [StringLength(25)]
        public string? TitleOfCourtesy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the hire date.
        /// </summary>
        public DateTime? HireDate { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [StringLength(60)]
        public string? Address { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [StringLength(15)]
        public string? City { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        [StringLength(15)]
        public string? Region { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        [StringLength(10)]
        public string? PostalCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [StringLength(15)]
        public string? Country { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the home phone.
        /// </summary>
        [StringLength(24)]
        public string? HomePhone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the extension.
        /// </summary>
        [StringLength(4)]
        public string? Extension { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        public byte[]? Photo { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        public string? Notes { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the reports to.
        /// </summary>
        public int? ReportsTo { get; set; }

        /// <summary>
        /// Gets or sets the photo path.
        /// </summary>
        [StringLength(255)]
        public string? PhotoPath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the reports to employee.
        /// </summary>
        [ForeignKey("ReportsTo")]
        public virtual Employee? Manager { get; set; }

        /// <summary>
        /// Gets or sets the territories.
        /// </summary>
        //public virtual ICollection<EmployeeTerritory> EmployeeTerritories { get; set; }

        /// <summary>
        /// Gets or sets the orders.
        /// </summary>
        //public virtual ICollection<Order> Orders { get; set; }

        /// <summary>
        /// Gets or sets the reporting employees.
        /// </summary>
        public virtual ICollection<Employee>? Workers { get; set; }
    }
}
