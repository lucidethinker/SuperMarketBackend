using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SuperMarketBackend.DTO
{
    public class ProductCategoryDTO
    {
        public int ProductCategoryId { get; set; }
        [BindRequired]
        public string? CategoryName { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public bool IsDeleted { get; set; } 
    }
}
