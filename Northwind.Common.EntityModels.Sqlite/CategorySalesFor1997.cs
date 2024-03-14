using Microsoft.EntityFrameworkCore;

namespace Packt.Shared;

[Keyless]
public partial class CategorySalesFor1997
{
    public string? CategoryName { get; set; }

    public double? CategorySales { get; set; }
}
