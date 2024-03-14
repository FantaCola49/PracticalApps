using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared;

[Keyless]
public partial class ProductsAboveAveragePrice
{
    [StringLength(40)]
    public string ProductName { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal? UnitPrice { get; set; }
}
