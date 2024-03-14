using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared;

[Keyless]
public partial class SalesTotalsByAmount
{
    public double? SaleAmount { get; set; }

    [Column("OrderID")]
    public int? OrderId { get; set; }

    public string? CompanyName { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime? ShippedDate { get; set; }
}
