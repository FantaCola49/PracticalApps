using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared;

[Keyless]
public partial class SummaryOfSalesByYear
{
    [Column(TypeName = "DATETIME")]
    public DateTime? ShippedDate { get; set; }

    [Column("OrderID")]
    public int? OrderId { get; set; }

    public double? Subtotal { get; set; }
}
