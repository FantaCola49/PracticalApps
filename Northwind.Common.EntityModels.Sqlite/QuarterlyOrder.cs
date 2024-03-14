using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared;

[Keyless]
public partial class QuarterlyOrder
{
    [Column("CustomerID")]
    public string? CustomerId { get; set; }

    public string? CompanyName { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }
}
