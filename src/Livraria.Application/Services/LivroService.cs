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
        private readonly IMapper _mapper;

        public LivroService(ILivroRepository livroRepository, IMapper mapper)
        {
            _livroRepository = livroRepository;
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

