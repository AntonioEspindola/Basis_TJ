using Livraria.Domain.Entities;
using MediatR;

namespace Livraria.Application.Products.Queries;

public class GetProductByIdQuery : IRequest<Product>
{
    public int Id { get; set; }
    public GetProductByIdQuery(int id)
    {
        Id = id;
    }
}
