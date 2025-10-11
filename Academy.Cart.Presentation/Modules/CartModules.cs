using Academy.Cart.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Academy.Cart.Presentation.Modules
{
    public static class CartModules
    {
        private const string BASE_URL = "api/v1/cart/";

        
        public static void AddCartModules(this IEndpointRouteBuilder app)
        {
            var cartGroup = app.MapGroup(BASE_URL);

            cartGroup.MapGet("getProducts", GetProducts)
                     .WithSummary("Gets the products and cart total per user.")
                     .WithDescription("Returns the items in the cart with the subtotal, tax, and total due.");
        }

        
        private static async Task<IResult> GetProducts(
            [FromQuery] int userId,
            ISender sender)
        {
            try
            {
                var query = new GetProductsQuery(userId);
                var result = await sender.Send(query);

                if (result == null || result.Products == null || !result.Products.Any())
                {
                    return Results.NotFound(new { message = "The cart is empty or the user does not exist." });
                }

                return Results.Ok(result);
            }
            catch (Exception ex)
            {
               
                return Results.Problem(
                    title: "Internal server error",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }
    }
}
