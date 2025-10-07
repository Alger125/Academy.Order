using Academy.Cart.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Academy.Cart.Infrastructure.Persistence
{
    public class CartDbContext : DbContext
    {
        public CartDbContext(DbContextOptions<CartDbContext> options) : base(options) { }

        public DbSet<CartItem> CartItems { get; set; }
    }
}
