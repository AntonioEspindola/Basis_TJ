using Livraria.Application.Products.Queries;
using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using MediatR;

namespace Livraria.Application.Products.Handlers;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        return await _productRepository.GetProductsAsync();
    }
}
