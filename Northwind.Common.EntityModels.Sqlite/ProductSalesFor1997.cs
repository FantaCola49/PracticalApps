using Microsoft.EntityFrameworkCore;

namespace Packt.Shared;

[Keyless]
public partial class ProductSalesFor1997
{
    public string? CategoryName { get; set; }

    public string? ProductName { get; set; }

    public double? ProductSales { get; set; }
}
