using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared;

[Keyless]
public partial class SalesByCategory
{
    [Column("CategoryID")]
    public int? CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string? ProductName { get; set; }

    public double? ProductSales { get; set; }
}
