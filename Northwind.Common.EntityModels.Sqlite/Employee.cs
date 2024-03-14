using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared;

public partial class Employee
{
    [Key]
    [Column("EmployeeID")]
    public int EmployeeId { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public string? FirstName { get; set; }

    public string? Title { get; set; }

    public string? TitleOfCourtesy { get; set; }

    [Column(TypeName = "DATE")]
    public DateOnly? BirthDate { get; set; }

    [Column(TypeName = "DATE")]
    public DateOnly? HireDate { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Region { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public string? HomePhone { get; set; }

    public string? Extension { get; set; }

    public byte[]? Photo { get; set; }

    public string? Notes { get; set; }

    public int? ReportsTo { get; set; }

    public string? PhotoPath { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("InverseEmployeeNavigation")]
    public virtual Employee EmployeeNavigation { get; set; } = null!;

    [InverseProperty("EmployeeNavigation")]
    public virtual Employee? InverseEmployeeNavigation { get; set; }

    [InverseProperty("Employee")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [ForeignKey("EmployeeId")]
    [InverseProperty("Employees")]
    public virtual ICollection<Territory> Territories { get; set; } = new List<Territory>();
}
