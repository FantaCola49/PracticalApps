using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared;

public partial class Product
{
    [Key]
    [Column("ProductID")]
    public int ProductId { get; set; }
    [Required]
    public string ProductName { get; set; } = null!;

    [Column("SupplierID")]
    public int? SupplierId { get; set; }

    [Column("CategoryID")]
    public int? CategoryId { get; set; }

    public string? QuantityPerUnit { get; set; }

    [Column(TypeName = "NUMERIC")]
    public double? UnitPrice { get; set; }

    public int? UnitsInStock { get; set; }

    public int? UnitsOnOrder { get; set; }

    public int? ReorderLevel { get; set; }

    public string Discontinued { get; set; } = null!;

    [InverseProperty("Product")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [ForeignKey("ProductId")]
    [InverseProperty("Product")]
    public virtual Category ProductNavigation { get; set; } = null!;

    [ForeignKey("SupplierId")]
    [InverseProperty("Products")]
    public virtual Supplier? Supplier { get; set; }
}
