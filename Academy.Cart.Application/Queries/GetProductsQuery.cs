using Academy.Cart.Application.DTOs;
using MediatR;

namespace Academy.Cart.Application.Queries
{
    public class GetProductsQuery : IRequest<CartResponseDto>
    {
        public int UserId { get; }

        public GetProductsQuery(int userId)
        {
            UserId = userId;
        }
    }
}
