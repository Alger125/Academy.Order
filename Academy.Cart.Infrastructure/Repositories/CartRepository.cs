using Academy.Cart.Application.Interfaces;
using Academy.Cart.Domain.Entities;
using Academy.Cart.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Academy.Cart.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly CartDbContext _context;

        public CartRepository(CartDbContext context)
        {
            _context = context;
        }

        public async Task<List<CartItem>> GetProductsByUserIdAsync(int userId)
        {
            return await _context.CartItems
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }
    }
}
