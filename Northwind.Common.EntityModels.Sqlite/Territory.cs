using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared;

public partial class Territory
{
    [Key]
    [Required]
    [Column("TerritoryID")]
    public string TerritoryId { get; set; } = null!;
    [Required]
    public string TerritoryDescription { get; set; } = null!;

    [Column("RegionID")]
    public int RegionId { get; set; }

    [ForeignKey("RegionId")]
    [InverseProperty("Territories")]
    public virtual Region Region { get; set; } = null!;

    [ForeignKey("TerritoryId")]
    [InverseProperty("Territories")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
