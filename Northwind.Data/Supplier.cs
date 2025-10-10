using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data
{
    [Table("Suppliers", Schema = "dbo")]
    public partial class Supplier
    {
        /// <summary>
        /// Gets or sets the supplier id.
        /// </summary>
        [Key]
        [Column("SupplierID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierId { get; set; }

        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the contact name.
        /// </summary>
        [StringLength(30)]
        public string ContactName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the contact title.
        /// </summary>
        [StringLength(30)]
        public string ContactTitle { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [StringLength(60)]
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [StringLength(15)]
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        [StringLength(15)]
        public string Region { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        [StringLength(10)]
        public string PostalCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [StringLength(15)]
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        [StringLength(24)]
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the fax.
        /// </summary>
        [StringLength(24)]
        public string Fax { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the home page.
        /// </summary>
        public string HomePage { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }
    }
}
