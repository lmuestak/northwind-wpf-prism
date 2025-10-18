using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data
{

    [Table("Shippers", Schema = "dbo")]
    public partial class Shipper
    {
        /// <summary>
        /// Gets or sets the shipper id.
        /// </summary>
        [Key]
        [Column("ShipperID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShipperId { get; set; }

        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        [Required]
        [StringLength(40)]
        public string? CompanyName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        [StringLength(24)]
        public string? Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the orders.
        /// </summary>
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
