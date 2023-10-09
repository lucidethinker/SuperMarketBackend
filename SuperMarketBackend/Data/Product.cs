using System;
using System.Collections.Generic;

namespace SuperMarketBackend.Data;

public partial class Product
{
    public int ProductId { get; set; }

    public int ProductCategoryId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public decimal? Tax { get; set; }

    public decimal? Discount { get; set; }

    public int StockQuantity { get; set; }

    public string Vendor { get; set; } = null!;

    public string? Image { get; set; }

    public bool IsDeleted { get; set; }

    public string? Descriptions { get; set; }

    public virtual ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();

    public virtual ProductCategory? ProductCategory { get; set; } = null!;
}
