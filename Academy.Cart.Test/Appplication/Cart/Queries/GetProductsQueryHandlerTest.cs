using Academy.Cart.Application.DTOs;
using Academy.Cart.Application.Handlers;
using Academy.Cart.Application.Interfaces;
using Academy.Cart.Application.Queries;
using Academy.Cart.Domain.Entities;
using Moq;

namespace Academy.Cart.Test.Appplication.Cart.Queries;

public class GetProductsQueryHandlerTest
{
    private readonly Mock<ICartRepository> _mockCart = new Mock<ICartRepository>();
    private readonly CancellationToken _cancellationToken = new();

    [Fact]
    public async Task Returns_Cart_Ok() 
    {
        //Arrange 
        int userId = 1;
        var cart = new List<CartItem>
            {
                new CartItem { 
                    Id = 1,
                    UserId = 1,
                    ProductId = 1, 
                    ProductName = "Product 1",
                    Description = "Description 1",
                    Image = "Image 1",
                    UnitPrice = 50,
                    Amount = 5,
                },
                new CartItem {
                    Id = 2,
                    UserId = 1,
                    ProductId = 2,
                    ProductName = "Product 2",
                    Description = "Description 2",
                    Image = "Image 2",
                    UnitPrice = 20,
                    Amount = 2,
                }
        };
        _mockCart.Setup(x => x.GetProductsByUserIdAsync(1)).ReturnsAsync(cart);
        //Act
        var query = new GetProductsQuery(userId);
        var queryHandler = new GetProductsQueryHandler(_mockCart.Object);
        var result = await queryHandler.Handle(query, _cancellationToken);

        //Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Products);
    }

    [Fact]
    public async Task Returns_Cart_Empty_Exception()
    {
        //Arrange 
        int userId = 1;
        var cart = new List<CartItem>();
        _mockCart.Setup(x => x.GetProductsByUserIdAsync(It.IsAny<int>())).ReturnsAsync(cart);
        //Act
        var query = new GetProductsQuery(userId);
        var queryHandler = new GetProductsQueryHandler(_mockCart.Object);

        //Assert
        try
        {
            await queryHandler.Handle(query, _cancellationToken);
        }
        catch (Exception ex)
        {
            Assert.Equal("El carrito está vacío o el usuario no existe.", ex.Message);
        }

    }

    [Fact]
    public async Task Returns_Id_Invalid_Exception()
    {
        //Arrange 
        int userId = 1;
        //Act
        var query = new GetProductsQuery(userId);
        var queryHandler = new GetProductsQueryHandler(_mockCart.Object);

        //Assert
        try
        {
            await queryHandler.Handle(query, _cancellationToken);
        }
        catch (Exception ex)
        {
            Assert.Equal("Id Invalido", ex.Message);
        }

    }

}
