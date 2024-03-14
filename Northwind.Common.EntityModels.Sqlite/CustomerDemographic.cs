using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared;

public partial class CustomerDemographic
{
    [Key]
    [Column("CustomerTypeID")]
    public string CustomerTypeId { get; set; } = null!;

    public string? CustomerDesc { get; set; }

    [ForeignKey("CustomerTypeId")]
    [InverseProperty("CustomerTypes")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
