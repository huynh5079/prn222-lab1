using System;
using System.Collections.Generic;

namespace ShopeeFoodChap2.Models;

public partial class Driver
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public int? CurrentOrderId { get; set; }

    public virtual Order? CurrentOrder { get; set; }
}
