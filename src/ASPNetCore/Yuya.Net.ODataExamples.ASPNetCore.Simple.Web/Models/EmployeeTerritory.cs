using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Models
{
    [Table("EmployeeTerritories")]
    public partial class EmployeeTerritory
    {
        /// <summary>
        /// Gets or sets the employee id.
        /// </summary>
        [Key, Column(Order = 0)]
        public int EmployeeID { get; set; }

        /// <summary>
        /// Gets or sets the territory id.
        /// </summary>
        [Key, Column(Order = 2)]
        [Required]
        [StringLength(20)]
        public string TerritoryID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Territory Territory { get; set; }
    }
}
