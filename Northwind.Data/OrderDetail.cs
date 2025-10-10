using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data
{

    [Table("Order Details", Schema = "dbo")]
    [PrimaryKey(nameof(OrderId), nameof(ProductId))]
    public partial class OrderDetail
    {
        /// <summary>
        /// Gets or sets the order id.
        /// </summary>
        [Column("OrderID",Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        [Column("ProductID", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the unit price.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        public short Quantity { get; set; }

        /// <summary>
        /// Gets or sets the discount.
        /// </summary>
        public float Discount { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        public virtual Order Order { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        public virtual Product Product { get; set; }
    }
}
