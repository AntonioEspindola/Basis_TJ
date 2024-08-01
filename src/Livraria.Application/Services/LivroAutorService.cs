using AutoMapper;
using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;

namespace Livraria.Application.Services
{
    public class LivroAutorService : ILivroAutorService
    {
        private readonly ILivroAutorRepository _livroAutorRepository;
        private readonly IMapper _mapper;

        public LivroAutorService(ILivroAutorRepository livroAutorRepository, IMapper mapper)
        {
            _livroAutorRepository = livroAutorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LivroAutorDTO>> GetLivroAutores()
        {
            var livroAutoresEntity = await _livroAutorRepository.GetLivroAutoresAsync();
            return _mapper.Map<IEnumerable<LivroAutorDTO>>(livroAutoresEntity);
        }

        public async Task<LivroAutorDTO> GetById(int livroId, int autorId)
        {
            var livroAutorEntity = await _livroAutorRepository.GetByIdAsync(livroId, autorId);
            return _mapper.Map<LivroAutorDTO>(livroAutorEntity);
        }

        public async Task Add(LivroAutorDTO livroAutorDto)
        {
            var livroAutorEntity = _mapper.Map<LivroAutor>(livroAutorDto);
            await _livroAutorRepository.CreateAsync(livroAutorEntity);
        }

        public async Task Update(LivroAutorDTO livroAutorDto)
        {
            var livroAutorEntity = _mapper.Map<LivroAutor>(livroAutorDto);
            await _livroAutorRepository.UpdateAsync(livroAutorEntity);
        }

        public async Task Remove(int livroId, int autorId)
        {
            var livroAutorEntity = await _livroAutorRepository.GetByIdAsync(livroId, autorId);
            await _livroAutorRepository.RemoveAsync(livroAutorEntity);
        }
    }
}

