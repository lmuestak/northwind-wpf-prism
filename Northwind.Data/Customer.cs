using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data
{
    [Table("Customers", Schema = "dbo")]
    public partial class Customer
    {
        /// <summary>
        /// Gets or sets the customer id.
        /// </summary>
        [Required]
        [StringLength(5, MinimumLength = 5)]
        [Key]
        [Column("CustomerID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        [StringLength(40)]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the contact name.
        /// </summary>
        [StringLength(30)]
        public string ContactName { get; set; }

        /// <summary>
        /// Gets or sets the contact title.
        /// </summary>
        [StringLength(30)]
        public string ContactTitle { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [StringLength(60)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [StringLength(15)]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        [StringLength(15)]
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        [StringLength(10)]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [StringLength(15)]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        [StringLength(24)]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the fax.
        /// </summary>
        [StringLength(24)]
        public string Fax { get; set; }

        /// <summary>
        /// Gets or sets the orders.
        /// </summary>
        public virtual ICollection<Order> Orders { get; set; }

        /// <summary>
        /// Gets or sets the customer demographics.
        /// </summary>
        //public virtual ICollection<CustomerDemographic> CustomerDemographics { get; set; }
    }
}
