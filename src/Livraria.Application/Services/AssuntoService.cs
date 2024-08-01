using AutoMapper;
using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;

namespace Livraria.Application.Services
{
    public class AssuntoService : IAssuntoService
    {
        private readonly IAssuntoRepository _assuntoRepository;
        private readonly IMapper _mapper;

        public AssuntoService(IAssuntoRepository assuntoRepository, IMapper mapper)
        {
            _assuntoRepository = assuntoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AssuntoDTO>> GetAssuntos()
        {
            var assuntosEntity = await _assuntoRepository.GetAssuntosAsync();
            return _mapper.Map<IEnumerable<AssuntoDTO>>(assuntosEntity);
        }

        public async Task<AssuntoDTO> GetById(int? id)
        {
            var assuntoEntity = await _assuntoRepository.GetByIdAsync(id);
            return _mapper.Map<AssuntoDTO>(assuntoEntity);
        }

        public async Task Add(AssuntoDTO assuntoDto)
        {
            var assuntoEntity = _mapper.Map<Assunto>(assuntoDto);
            await _assuntoRepository.CreateAsync(assuntoEntity);
            assuntoDto.Id = assuntoEntity.Id;
        }

        public async Task Update(AssuntoDTO assuntoDto)
        {
            var assuntoEntity = _mapper.Map<Assunto>(assuntoDto);
            await _assuntoRepository.UpdateAsync(assuntoEntity);
        }

        public async Task Remove(int? id)
        {
            var assuntoEntity = await _assuntoRepository.GetByIdAsync(id);
            await _assuntoRepository.RemoveAsync(assuntoEntity);
        }
    }
}

