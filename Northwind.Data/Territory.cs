using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data
{
    [Table("Territories", Schema = "dbo")]
    public partial class Territory
    {
        /// <summary>
        /// Gets or sets the territory id.
        /// </summary>
        [Required]
        [StringLength(20)]
        [Key]
        [Column("TerritoryID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string TerritoryId { get; set; }

        /// <summary>
        /// Gets or sets the territory description.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the region id.
        /// </summary>
        public int RegionID { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        public virtual Region Region { get; set; }

        /// <summary>
        /// Gets or sets the employee territories.
        /// </summary>
        public virtual ICollection<EmployeeTerritory> EmployeeTerritories { get; set; }
    }
}
