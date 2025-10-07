namespace Academy.Cart.Application.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Amount { get; set; }
        public decimal Subtotal { get; set; }
    }
}
