using AutoMapper;
using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Application.Services
{
    public class LivroPrecoCanalVendaService : ILivroPrecoCanalVendaService
    {
        private readonly ILivroPrecoCanalVendaRepository _livroPrecoCanalVendaRepository;
        private readonly IMapper _mapper;

        public LivroPrecoCanalVendaService(ILivroPrecoCanalVendaRepository livroPrecoCanalVendaRepository, IMapper mapper)
        {
            _livroPrecoCanalVendaRepository = livroPrecoCanalVendaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LivroPrecoCanalVendaDTO>> GetLivroPrecoCanalVenda()
        {
            var livroPrecoCanalVendaEntity = await _livroPrecoCanalVendaRepository.GetLivroPrecoCanalVendaAsync();
            return _mapper.Map<IEnumerable<LivroPrecoCanalVendaDTO>>(livroPrecoCanalVendaEntity);
        }

        public async Task<LivroPrecoCanalVendaDTO> GetById(int livroId, int canalVendaId)
        {
            var livroPrecoCanalVendaEntity = await _livroPrecoCanalVendaRepository.GetByIdAsync(livroId, canalVendaId);
            return _mapper.Map<LivroPrecoCanalVendaDTO>(livroPrecoCanalVendaEntity);
        }

        public async Task Add(LivroPrecoCanalVendaDTO livroPrecoCanalVendaDto)
        {
            var livroPrecoCanalVendaEntity = _mapper.Map<LivroPrecoCanalVenda>(livroPrecoCanalVendaDto);
            await _livroPrecoCanalVendaRepository.CreateAsync(livroPrecoCanalVendaEntity);
        }

        public async Task Update(LivroPrecoCanalVendaDTO livroPrecoCanalVendaDto)
        {
            var livroPrecoCanalVendaEntity = _mapper.Map<LivroPrecoCanalVenda>(livroPrecoCanalVendaDto);
            await _livroPrecoCanalVendaRepository.UpdateAsync(livroPrecoCanalVendaEntity);
        }

        public async Task Remove(int livroId, int canalVendaId)
        {
            var livroPrecoCanalVendaEntity = await _livroPrecoCanalVendaRepository.GetByIdAsync(livroId, canalVendaId);
            await _livroPrecoCanalVendaRepository.RemoveAsync(livroPrecoCanalVendaEntity);
        }
    }
}
