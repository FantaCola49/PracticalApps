using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared;

public partial class Region
{
    [Key]
    [Column("RegionID")]
    public int RegionId { get; set; }

    public string RegionDescription { get; set; } = null!;

    [InverseProperty("Region")]
    public virtual ICollection<Territory> Territories { get; set; } = new List<Territory>();
}
