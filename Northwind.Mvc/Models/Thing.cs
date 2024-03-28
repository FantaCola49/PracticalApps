using System.ComponentModel.DataAnnotations;

namespace Northwind.Mvc.Models;

public class Thing
{
    [Range(0, 10)]
    public int? Id { get; set; }
    [Required]
    public string? Color { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
}
