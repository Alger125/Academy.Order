using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Academy.Cart.Application.Handlers;
using Academy.Cart.Application.Interfaces;
using Academy.Cart.Application.DTOs;
using Academy.Cart.Application.Queries;
using Academy.Cart.Domain.Entities;

namespace Academy.Cart.Tests.Handlers
{
    public class GetProductsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnCartResponseDto_WhenUserHasProducts()
        {
            // Arrange
            int userId = 1;

            var mockRepo = new Mock<ICartRepository>();
            mockRepo.Setup(r => r.GetProductsByUserIdAsync(userId))
                .ReturnsAsync(new List<CartItem>
                {
                    new CartItem { ProductId = 100, ProductName = "Laptop", Description = "Gaming", Image = "laptop.png", UnitPrice = 20000m, Amount = 1 },
                    new CartItem { ProductId = 200, ProductName = "Mouse", Description = "Wireless", Image = "mouse.png", UnitPrice = 500m, Amount = 2 }
                });

            var handler = new GetProductsQueryHandler(mockRepo.Object);
            var query = new GetProductsQuery(userId);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(2, result.Products.Count);

            // Subtotal = 20000 + (500*2) = 21000
            // Tax = 21000 * 0.16 = 3360
            // Shipping = 150
            // Total = 24510
            Assert.Equal(21000m, result.Products.Sum(p => p.Subtotal));
            Assert.Equal(3360m, result.Tax);
            Assert.Equal(150m, result.ShippingCost);
            Assert.Equal(24510m, result.Total);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenCartIsEmpty()
        {
            // Arrange
            int userId = 99;

            var mockRepo = new Mock<ICartRepository>();
            mockRepo.Setup(r => r.GetProductsByUserIdAsync(userId))
                .ReturnsAsync(new List<CartItem>());

            var handler = new GetProductsQueryHandler(mockRepo.Object);
            var query = new GetProductsQuery(userId);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<System.Exception>(() => handler.Handle(query, CancellationToken.None));
            Assert.Equal("El carrito está vacío o el usuario no existe.", exception.Message);
        }
    }
}
