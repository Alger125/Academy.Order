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
                     .WithSummary("Obtiene los productos y el total del carrito por usuario.")
                     .WithDescription("Devuelve los productos del carrito con subtotal, impuestos y total a pagar.");
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
                    return Results.NotFound(new { message = "El carrito está vacío o el usuario no existe." });
                }

                return Results.Ok(result);
            }
            catch (Exception ex)
            {
               
                return Results.Problem(
                    title: "Error interno en el servidor",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }
    }
}
