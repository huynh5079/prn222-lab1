using System;
using System.Collections.Generic;

namespace ShopeeFoodChap2.Models;

public partial class Restaurant
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
