﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared;

[Keyless]
public partial class SummaryOfSalesByQuarter
{
    [Column(TypeName = "datetime")]
    public DateTime? ShippedDate { get; set; }

    [Column("OrderID")]
    public int OrderId { get; set; }

    [Column(TypeName = "money")]
    public decimal? Subtotal { get; set; }
}
