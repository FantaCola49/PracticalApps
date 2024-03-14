using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared;

[Keyless]
public partial class CurrentProductList
{
    [Column("ProductID")]
    public int? ProductId { get; set; }

    public string? ProductName { get; set; }
}
