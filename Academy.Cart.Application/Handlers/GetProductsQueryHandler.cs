using Academy.Cart.Application.DTOs;
using Academy.Cart.Application.Interfaces;
using Academy.Cart.Application.Queries;
using MediatR;

namespace Academy.Cart.Application.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, CartResponseDto>
    {
        private readonly ICartRepository _repository;

        public GetProductsQueryHandler(ICartRepository repository)
        {
            _repository = repository;
        }

        public async Task<CartResponseDto> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.GetProductsByUserIdAsync(request.UserId);

            if (items == null || !items.Any())
                throw new Exception("El carrito está vacío o el usuario no existe.");

            var products = items.Select(x => new ProductDto
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Description = x.Description,
                Image = x.Image,
                UnitPrice = x.UnitPrice,
                Amount = x.Amount,
                Subtotal = x.UnitPrice * x.Amount
            }).ToList();

            decimal subtotal = products.Sum(p => p.Subtotal);
            decimal tax = subtotal * 0.16m;
            decimal shipping = 150m;
            decimal total = subtotal + tax + shipping;

            return new CartResponseDto
            {
                UserId = request.UserId,
                Products = products,
                ShippingCost = shipping,
                Tax = tax,
                Total = total
            };
        }
    }
}
