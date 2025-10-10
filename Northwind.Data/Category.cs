using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Northwind.Data
{
    [Table("Categories", Schema = "dbo")]
    public class Category
    {
        [Key]
        [Column("CategoryID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category name.
        /// </summary>
        [Required]
        [StringLength(15)]
        [Column("CategoryName")]
        public string CategoryName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string? Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the picture.
        /// </summary>
        public byte[]? Picture { get; set; }
        public byte[]? Icon17 { get; set; }
        public byte[]? Icon25 { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        public virtual ICollection<Product>? Products { get; set; }
    }
}