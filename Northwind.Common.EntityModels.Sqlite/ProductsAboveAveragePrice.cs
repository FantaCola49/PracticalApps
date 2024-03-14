using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared;

[Keyless]
public partial class ProductsAboveAveragePrice
{
    public string? ProductName { get; set; }

    [Column(TypeName = "NUMERIC")]
    public double? UnitPrice { get; set; }
}
