using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SuperMarketBackend.DTO
{
    public class OrderDTO
    {
        public int OrderId {  get; set; }
        [BindRequired]
        public int UserId { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        [BindRequired]
        public decimal TotalAmount { get; set; }
        public int Status {  get; set; }
        public string? OrderDate {  get; set; }
        public bool IsDeleted {  get; set; }
        public List<OrderItemDTO>? OrderItems {  get; set; }
    }
}
