using AutoMapper;
using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Application.Services
{
    public class CanalVendaService : ICanalVendaService
    {
        private readonly ICanalVendaRepository _canalVendaRepository;
        private readonly IMapper _mapper;

        public CanalVendaService(ICanalVendaRepository canalVendaRepository, IMapper mapper)
        {
            _canalVendaRepository = canalVendaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CanalVendaDTO>> GetCanalVendas()
        {
            var canalVendasEntity = await _canalVendaRepository.GetCanalVendaAsync();
            return _mapper.Map<IEnumerable<CanalVendaDTO>>(canalVendasEntity);
        }

        public async Task<CanalVendaDTO> GetById(int id)
        {
            var canalVendaEntity = await _canalVendaRepository.GetByIdAsync(id);
            return _mapper.Map<CanalVendaDTO>(canalVendaEntity);
        }

        public async Task Add(CanalVendaDTO canalVendaDto)
        {
            var canalVendaEntity = _mapper.Map<CanalVenda>(canalVendaDto);
            await _canalVendaRepository.CreateAsync(canalVendaEntity);
            canalVendaDto.Id = canalVendaEntity.Id;
        }

        public async Task Update(CanalVendaDTO canalVendaDto)
        {
            var canalVendaEntity = _mapper.Map<CanalVenda>(canalVendaDto);
            await _canalVendaRepository.UpdateAsync(canalVendaEntity);
        }

        public async Task Remove(int id)
        {
            var canalVendaEntity = await _canalVendaRepository.GetByIdAsync(id);
            await _canalVendaRepository.RemoveAsync(canalVendaEntity);
        }
    }
}

