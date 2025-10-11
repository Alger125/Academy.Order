using Xunit;
using Academy.Cart.Domain.Entities;

namespace Academy.Cart.Tests.Domain
{
    public class CartItemTests
    {
        [Fact]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            // Arrange & Act
            var item = new CartItem
            {
                Id = 1,
                UserId = 10,
                ProductId = 99,
                ProductName = "Monitor",
                Description = "Monitor 27 pulgadas",
                Image = "monitor.png",
                UnitPrice = 3500.50m,
                Amount = 2
            };

            // Assert
            Assert.Equal(1, item.Id);
            Assert.Equal(10, item.UserId);
            Assert.Equal(99, item.ProductId);
            Assert.Equal("Monitor", item.ProductName);
            Assert.Equal("Monitor 27 pulgadas", item.Description);
            Assert.Equal("monitor.png", item.Image);
            Assert.Equal(3500.50m, item.UnitPrice);
            Assert.Equal(2, item.Amount);
        }

        [Fact]
        public void TotalCost_ShouldBeCalculatedCorrectly()
        {
            // Arrange
            var item = new CartItem
            {
                UnitPrice = 100m,
                Amount = 3
            };

            // Act
            var total = item.UnitPrice * item.Amount;

            // Assert
            Assert.Equal(300m, total);
        }

        [Fact]
        public void ShouldAllowUpdatingProperties()
        {
            // Arrange
            var item = new CartItem();

            // Act
            item.ProductName = "Mouse Gamer";
            item.UnitPrice = 550m;
            item.Amount = 1;

            // Assert
            Assert.Equal("Mouse Gamer", item.ProductName);
            Assert.Equal(550m, item.UnitPrice);
            Assert.Equal(1, item.Amount);
        }
    }
}
