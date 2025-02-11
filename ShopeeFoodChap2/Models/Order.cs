using System;
using System.Collections.Generic;

namespace ShopeeFoodChap2.Models;

public partial class Order
{
    public int Id { get; set; }

    public string OrderNumber { get; set; } = null!;

    public string? OrderDetails { get; set; }

    public string? DeliveryAddress { get; set; }

    public DateTime OrderDate { get; set; }

    public int? RestaurantId { get; set; }

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    public virtual Restaurant? Restaurant { get; set; }
}
