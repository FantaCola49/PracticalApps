﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization; // [XnlIgnore]

namespace Packt.Shared;

[Index("City", Name = "City")]
[Index("CompanyName", Name = "CompanyName")]
[Index("PostalCode", Name = "PostalCode")]
[Index("Region", Name = "Region")]
public partial class Customer
{
    [Key]
    [StringLength(5)]
    [Required]
    [RegularExpression("[A-Z]{5}")]
    public string CustomerId { get; set; } = null!;

    [StringLength(40)]
    public string CompanyName { get; set; } = null!;

    [StringLength(30)]
    public string? ContactName { get; set; }

    [StringLength(30)]
    public string? ContactTitle { get; set; }

    [StringLength(60)]
    public string? Address { get; set; }

    [StringLength(15)]
    public string? City { get; set; }

    [StringLength(15)]
    public string? Region { get; set; }

    [StringLength(10)]
    public string? PostalCode { get; set; }

    [StringLength(15)]
    public string? Country { get; set; }

    [StringLength(24)]
    public string? Phone { get; set; }

    [StringLength(24)]
    public string? Fax { get; set; }

    [XmlIgnore]
    [InverseProperty("Customer")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [XmlIgnore]
    [ForeignKey("CustomerId")]
    [InverseProperty("Customers")]
    public virtual ICollection<CustomerDemographic> CustomerTypes { get; set; } = new List<CustomerDemographic>();
}
