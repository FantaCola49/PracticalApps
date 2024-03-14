using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared;

[PrimaryKey("OrderId", "ProductId")]
[Table("Order Details")]
public partial class OrderDetail
{
    [Key]
    [Column("OrderID")]
    public int OrderId { get; set; }

    [Key]
    [Column("ProductID")]
    public int ProductId { get; set; }

    [Column(TypeName = "NUMERIC")]
    public double UnitPrice { get; set; }

    public short Quantity { get; set; }

    public double Discount { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderDetails")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("OrderDetails")]
    public virtual Product Product { get; set; } = null!;
}
