﻿using Microsoft.EntityFrameworkCore;

namespace Packt.Shared;

[Keyless]
public partial class CustomerAndSuppliersByCity
{
    public string? City { get; set; }

    public string? CompanyName { get; set; }

    public string? ContactName { get; set; }

    public string? Relationship { get; set; }
}
