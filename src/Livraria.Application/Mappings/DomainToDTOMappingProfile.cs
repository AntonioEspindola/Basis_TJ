using AutoMapper;
using Livraria.Application.DTOs;
using Livraria.Domain.Entities;

namespace Livraria.Application.Mappings;

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<Assunto, AssuntoDTO>().ReverseMap();
        CreateMap<Autor, AutorDTO>().ReverseMap();
        CreateMap<Livro, LivroDTO>().ReverseMap();
        CreateMap<LivroAssunto, LivroAssuntoDTO>().ReverseMap();
        CreateMap<LivroAutor, LivroAutorDTO>().ReverseMap();
        CreateMap<LivroAutorAssunto, LivroAutorAssuntoDTO>().ReverseMap();
        CreateMap<LivroPrecoCanalVenda, LivroPrecoCanalVendaDTO>().ReverseMap();
        CreateMap<CanalVenda, CanalVendaDTO>().ReverseMap();
    }
}
