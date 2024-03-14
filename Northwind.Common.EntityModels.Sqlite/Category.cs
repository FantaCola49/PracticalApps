using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared;

public partial class Category
{
    [Key]
    [Column("CategoryID")]
    public int CategoryId { get; set; }
    [Required]
    public string? CategoryName { get; set; }

    public string? Description { get; set; }

    public byte[]? Picture { get; set; }

    [InverseProperty("ProductNavigation")]
    public virtual Product? Product { get; set; }
}
