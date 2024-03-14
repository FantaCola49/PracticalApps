using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared;

[Keyless]
public partial class OrderDetailsExtended
{
    [Column("OrderID")]
    public int? OrderId { get; set; }

    [Column("ProductID")]
    public int? ProductId { get; set; }

    public string? ProductName { get; set; }

    [Column(TypeName = "NUMERIC")]
    public double? UnitPrice { get; set; }

    public int? Quantity { get; set; }

    public double? Discount { get; set; }

    public double? ExtendedPrice { get; set; }
}
