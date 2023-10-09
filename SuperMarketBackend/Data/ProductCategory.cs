using System;
using System.Collections.Generic;

namespace SuperMarketBackend.Data;

public partial class ProductCategory
{
    public int ProductCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public decimal? Tax { get; set; }

    public decimal? Discount { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
}
