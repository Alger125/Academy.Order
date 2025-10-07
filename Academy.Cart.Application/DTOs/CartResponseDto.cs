using System.Collections.Generic;

namespace Academy.Cart.Application.DTOs
{
    public class CartResponseDto
    {
        public int UserId { get; set; }
        public List<ProductDto> Products { get; set; } = new();
        public decimal ShippingCost { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
    }
}
