using Livraria.Application.Products.Queries;
using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using MediatR;

namespace Livraria.Application.Products.Handlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
{
    private readonly IProductRepository _productRepository;
    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Handle(GetProductByIdQuery request,
         CancellationToken cancellationToken)
    {
        return await _productRepository.GetByIdAsync(request.Id);
    }
}
