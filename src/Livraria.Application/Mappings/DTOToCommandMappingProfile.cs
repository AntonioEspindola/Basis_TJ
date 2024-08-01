using AutoMapper;
using Livraria.Application.DTOs;
using Livraria.Application.Products.Commands;

namespace Livraria.Application.Mappings;

public class DTOToCommandMappingProfile : Profile
{
    public DTOToCommandMappingProfile()
    {
        CreateMap<ProductDTO, ProductCreateCommand>();
        CreateMap<ProductDTO, ProductUpdateCommand>();
    }
}
