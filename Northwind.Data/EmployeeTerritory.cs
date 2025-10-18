using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data
{
    [Table("EmployeeTerritories", Schema = "dbo")]
    [PrimaryKey(nameof(EmployeeId), nameof(TerritoryId))]
    public partial class EmployeeTerritory
    {
        /// <summary>
        /// Gets or sets the employee id.
        /// </summary>
        [Column("EmployeeID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the territory id.
        /// </summary>
        [Column("TerritoryID", Order = 2)]
        [Required]
        [StringLength(20)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string TerritoryId { get; set; } = string.Empty;

        public virtual Employee? Employee { get; set; }

        public virtual Territory? Territory { get; set; }
    }
}
