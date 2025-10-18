using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data
{
    [Table("Region", Schema = "dbo")]
    public partial class Region
    {
        /// <summary>
        /// Gets or sets the region id.
        /// </summary>
        [Column("RegionID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RegionId { get; set; }

        /// <summary>
        /// Gets or sets the region description.
        /// </summary>
        [Required]
        [StringLength(50)]
        [Column("RegionDescription")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the territories.
        /// </summary>
        public virtual ICollection<Territory>? Territories { get; set; }
    }
}
