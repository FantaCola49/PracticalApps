using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared;

public partial class Order
{
    [Key]
    [Column("OrderID")]
    public int OrderId { get; set; }

    [Column("CustomerID")]
    [RegularExpression("[A-Z]{5}")]
    public string? CustomerId { get; set; }

    [Column("EmployeeID")]
    public int? EmployeeId { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime? OrderDate { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime? RequiredDate { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime? ShippedDate { get; set; }

    public int? ShipVia { get; set; }

    [Column(TypeName = "NUMERIC")]
    public decimal? Freight { get; set; }

    public string? ShipName { get; set; }

    public string? ShipAddress { get; set; }

    public string? ShipCity { get; set; }

    public string? ShipRegion { get; set; }

    public string? ShipPostalCode { get; set; }

    public string? ShipCountry { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Orders")]
    public virtual Customer? Customer { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("Orders")]
    public virtual Employee? Employee { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [ForeignKey("ShipVia")]
    [InverseProperty("Orders")]
    public virtual Shipper? ShipViaNavigation { get; set; }
}
