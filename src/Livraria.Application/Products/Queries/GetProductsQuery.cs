using Livraria.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Livraria.Application.Products.Queries;

public class GetProductsQuery : IRequest<IEnumerable<Product>>
{
}
