using AutoMapper;
using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;

namespace Livraria.Application.Services
{
    public class LivroAutorAssuntoService : ILivroAutorAssuntoService
    {
        private readonly ILivroAutorAssuntoRepository _livroAutorAssuntoRepository;
        private readonly IMapper _mapper;

        public LivroAutorAssuntoService(ILivroAutorAssuntoRepository livroAutorAssuntoRepositoryRepository, IMapper mapper)
        {
            _livroAutorAssuntoRepository = livroAutorAssuntoRepositoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LivroAutorAssuntoDTO>> GetRelatorioLivros()
        {
            var livroAutorAssuntoEntity = await _livroAutorAssuntoRepository.GetRelatorioLivros();
            return _mapper.Map<IEnumerable<LivroAutorAssuntoDTO>>(livroAutorAssuntoEntity);
        }
              
    }
}

