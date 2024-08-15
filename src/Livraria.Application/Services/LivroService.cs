using AutoMapper;
using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;

namespace Livraria.Application.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;
        private readonly ILivroPrecoCanalVendaRepository _livroPrecoCanalVendaRepository;
        private readonly ILivroAssuntoRepository _livroAssuntoRepository;
        private readonly ILivroAutorRepository _livroAutorRepository;

        private readonly IMapper _mapper;

        public LivroService(ILivroRepository livroRepository, 
                            ILivroPrecoCanalVendaRepository livroPrecoCanalVendaRepository,
                            ILivroAssuntoRepository livroAssuntoRepository,
                            ILivroAutorRepository livroAutorRepository,
                            IMapper mapper)
        {
            _livroRepository = livroRepository;
            _livroAssuntoRepository = livroAssuntoRepository;
            _livroAutorRepository = livroAutorRepository;
            _livroPrecoCanalVendaRepository = livroPrecoCanalVendaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LivroDTO>> GetLivros()
        {
            var livrosEntity = await _livroRepository.GetLivrosAsync();
            return _mapper.Map<IEnumerable<LivroDTO>>(livrosEntity);
        }

        public async Task<LivroDTO> GetById(int? id)
        {
            var livroEntity = await _livroRepository.GetByIdAsync(id);
            return _mapper.Map<LivroDTO>(livroEntity);
        }

        public async Task Add(LivroDTO livroDto)
        {
            
                var livroEntity = _mapper.Map<Livro>(livroDto);
                await _livroRepository.CreateAsync(livroEntity);
                livroDto.Id = livroEntity.Id;
            
        }

        public async Task Add(LivroDTO livroDto, 
            List<LivroPrecoCanalVendaDTO> precosCanalVendaDto,
            List<LivroAssuntoDTO> livroAssuntoDTO,
            List<LivroAutorDTO> livroAutorDTO)
        {
            var livroEntity = _mapper.Map<Livro>(livroDto);
            livroEntity = await _livroRepository.CreateAsync(livroEntity);
            livroDto.Id = livroEntity.Id;

            var livroAssuntoEntities = livroAssuntoDTO
               .Select(dto => new LivroAssunto {Livro_Codl = livroEntity.Id,Assunto_CodAs = dto.Assunto_CodAs })
               .ToList();

            var livroAutorEntities = livroAutorDTO
              .Select(dto => new LivroAutor { Livro_Codl = livroEntity.Id, Autor_CodAu = dto.Autor_CodAu })
              .ToList();

            var precosCanalVendaEntities = precosCanalVendaDto
                .Select(dto => new LivroPrecoCanalVenda(livroDto.Id, dto.CanalVenda_CodCanal, dto.PrecoVenda))
                .ToList();

            foreach (var precoCanalVenda in precosCanalVendaEntities)
            {
                precoCanalVenda.Update(livroEntity.Id, precoCanalVenda.CanalVenda_CodCanal, precoCanalVenda.PrecoVenda);
            }

            await _livroPrecoCanalVendaRepository.AddRangePrecoCanalVendaAsync(precosCanalVendaEntities);
            await _livroAssuntoRepository.AddRangeAsync(livroAssuntoEntities);
            await _livroAutorRepository.AddRangeAsync(livroAutorEntities);

        }


        public async Task Update(LivroDTO livroDto)
        {
            var livroEntity = _mapper.Map<Livro>(livroDto);
            await _livroRepository.UpdateAsync(livroEntity);
        }

        public async Task Remove(int? id)
        {
            var livroEntity = await _livroRepository.GetByIdAsync(id);
            await _livroRepository.RemoveAsync(livroEntity);
        }
    }
}

