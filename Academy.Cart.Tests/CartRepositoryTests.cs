using Xunit;
using Microsoft.EntityFrameworkCore;
using Academy.Cart.Infrastructure.Repositories;
using Academy.Cart.Infrastructure.Persistence;
using Academy.Cart.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Academy.Cart.Tests.Repositories
{
    public class CartRepositoryTests
    {
        private CartDbContext CreateDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<CartDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new CartDbContext(options);
        }

        [Fact]
        public async Task GetProductsByUserIdAsync_ShouldReturnOnlyItemsForSpecifiedUser()
        {
            // Arrange
            var dbContext = CreateDbContext("CartDbTest1");

            dbContext.CartItems.AddRange(new List<CartItem>
            {
                new CartItem { UserId = 1, ProductId = 100, ProductName = "Mouse", Description = "Mouse óptico", Image = "mouse.png", UnitPrice = 150m, Amount = 1 },
                new CartItem { UserId = 1, ProductId = 200, ProductName = "Keyboard", Description = "Teclado mecánico", Image = "keyboard.png", UnitPrice = 350m, Amount = 2 },
                new CartItem { UserId = 2, ProductId = 300, ProductName = "Monitor", Description = "Monitor LED", Image = "monitor.png", UnitPrice = 2000m, Amount = 1 }
            });

            await dbContext.SaveChangesAsync();

            var repository = new CartRepository(dbContext);

            // Act
            var result = await repository.GetProductsByUserIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, item => Assert.Equal(1, item.UserId));
        }

        [Fact]
        public async Task GetProductsByUserIdAsync_ShouldReturnEmptyList_WhenUserHasNoProducts()
        {
            // Arrange
            var dbContext = CreateDbContext("CartDbTest2");

            dbContext.CartItems.Add(new CartItem
            {
                UserId = 10,
                ProductId = 500,
                ProductName = "Tablet",
                Description = "Tablet de 10 pulgadas",
                Image = "tablet.png",
                UnitPrice = 1000m,
                Amount = 1
            });

            await dbContext.SaveChangesAsync();

            var repository = new CartRepository(dbContext);

            // Act
            var result = await repository.GetProductsByUserIdAsync(99); // usuario inexistente

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
