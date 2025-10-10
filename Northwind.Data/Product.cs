using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data
{
    [Table("Products", Schema = "dbo")]
    public partial class Product
    {
        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        [Key]
        [Column("ProductID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        [Required]
        [StringLength(40)]
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the supplier id.
        /// </summary>
        public int? SupplierId { get; set; }

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the quantity per unit.
        /// </summary>
        [StringLength(20)]
        public string QuantityPerUnit { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unit price.
        /// </summary>
        public decimal? UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the units in stock.
        /// </summary>
        public short? UnitsInStock { get; set; }

        /// <summary>
        /// Gets or sets the units on order.
        /// </summary>
        public short? UnitsOnOrder { get; set; }

        /// <summary>
        /// Gets or sets the reorder level.
        /// </summary>
        public short? ReorderLevel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether discontinued.
        /// </summary>
        public bool Discontinued { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// Gets or sets the supplier.
        /// </summary>
        public virtual Supplier Supplier { get; set; }

        /// <summary>
        /// Gets or sets the order details.
        /// </summary>
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
