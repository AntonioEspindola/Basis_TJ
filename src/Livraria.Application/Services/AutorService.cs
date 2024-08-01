using AutoMapper;
using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;

namespace Livraria.Application.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IMapper _mapper;

        public AutorService(IAutorRepository autorRepository, IMapper mapper)
        {
            _autorRepository = autorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AutorDTO>> GetAutores()
        {
            var autoresEntity = await _autorRepository.GetAutoresAsync();
            return _mapper.Map<IEnumerable<AutorDTO>>(autoresEntity);
        }

        public async Task<AutorDTO> GetById(int? id)
        {
            var autorEntity = await _autorRepository.GetByIdAsync(id);
            return _mapper.Map<AutorDTO>(autorEntity);
        }

        public async Task Add(AutorDTO autorDto)
        {
            var autorEntity = _mapper.Map<Autor>(autorDto);
            await _autorRepository.CreateAsync(autorEntity);
            autorDto.Id = autorEntity.Id;
        }

        public async Task Update(AutorDTO autorDto)
        {
            var autorEntity = _mapper.Map<Autor>(autorDto);
            await _autorRepository.UpdateAsync(autorEntity);
        }

        public async Task Remove(int? id)
        {
            var autorEntity = await _autorRepository.GetByIdAsync(id);
            await _autorRepository.RemoveAsync(autorEntity);
        }
    }
}

