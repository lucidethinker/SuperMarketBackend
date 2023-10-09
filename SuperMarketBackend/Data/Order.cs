using System;
using System.Collections.Generic;

namespace SuperMarketBackend.Data;

public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public decimal GrossAmount { get; set; }

    public decimal? TaxAmount { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? TotalAmount { get; set; }

    public int Status { get; set; }

    public bool IsDeleted { get; set; }
    public string? OrderDate {  get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User User { get; set; } = null!;
}
