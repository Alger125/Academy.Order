using Academy.Cart.Domain.Entities;

namespace Academy.Cart.Application.Interfaces
{
    public interface ICartRepository
    {
        Task<List<CartItem>> GetProductsByUserIdAsync(int userId);
    }
}
