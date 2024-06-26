﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Packt.Shared;

public partial class Customer
{
    [Key]
    [Column("CustomerID")]
    public string CustomerId { get; set; } = null!;
    [Required]
    [Column(TypeName = "nvarchar (40)")]
    [StringLength(50)]
    public string? CompanyName { get; set; }

    public string? ContactName { get; set; }

    public string? ContactTitle { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Region { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public string? Phone { get; set; }

    public string? Fax { get; set; }

    [XmlIgnore]
    [InverseProperty("Customer")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [XmlIgnore]
    [ForeignKey("CustomerId")]
    [InverseProperty("Customers")]
    public virtual ICollection<CustomerDemographic> CustomerTypes { get; set; } = new List<CustomerDemographic>();
}
