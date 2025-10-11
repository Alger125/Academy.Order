using Microsoft.AspNetCore.Routing;

namespace Academy.Cart.Presentation.Modules
{
    public static class ModulesConfiguration
    {
        
        public static void MapPresentationEndpoints(this IEndpointRouteBuilder app)
        {
            app.AddCartModules();
        }
    }
}