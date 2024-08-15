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
        CreateMap<CanalVenda, CanalVendaDTO>().ReverseMap();

        CreateMap<LivroPrecoCanalVenda, LivroPrecoCanalVendaDTO>();
            //.ForMember(dest => dest.Livro_Codl, opt => opt.Ignore()) // Ignora a validação do ID durante o mapeamento
            //.ForMember(dest => dest.CanalVenda_CodCanal, opt => opt.Ignore())
            //.ForMember(dest => dest.PrecoVenda, opt => opt.MapFrom(src => src.PrecoVenda))
            //.AfterMap((src, dest, context) =>
            //{
            //    if (context.Options.Items.TryGetValue("LivroId", out var livroId))
            //    {
            //        dest.Update((int)livroId, src.CanalVenda_CodCanal, src.PrecoVenda);
            //    }
            //});
    }
}
