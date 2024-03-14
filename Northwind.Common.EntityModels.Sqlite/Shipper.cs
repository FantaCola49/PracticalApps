using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared;

public partial class Shipper
{
    [Key]
    [Column("ShipperID")]
    public int ShipperId { get; set; }
    [Required]
    public string CompanyName { get; set; } = null!;

    public string? Phone { get; set; }

    [InverseProperty("ShipViaNavigation")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
