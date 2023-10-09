using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SuperMarketBackend.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        [BindRequired]
        public int ProductCategoryId {  get; set; }
        [BindRequired]
        public string? ProductName { get; set; }
        [BindRequired]
        public decimal Price { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        [BindRequired]
        public int StockQuantity { get; set; }
        public string? Descriptions { get; set; }
        public bool IsDeleted {  get; set; }
        public  string? Vendor { get; set; }
        public string? Image { get; set; }

    }
}
