using AutoMapper;
using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;

namespace Livraria.Application.Services
{
    public class LivroAssuntoService : ILivroAssuntoService
    {
        private readonly ILivroAssuntoRepository _livroAssuntoRepository;
        private readonly IMapper _mapper;

        public LivroAssuntoService(ILivroAssuntoRepository livroAssuntoRepository, IMapper mapper)
        {
            _livroAssuntoRepository = livroAssuntoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LivroAssuntoDTO>> GetLivroAssuntos()
        {
            var livroAssuntosEntity = await _livroAssuntoRepository.GetLivroAssuntosAsync();
            return _mapper.Map<IEnumerable<LivroAssuntoDTO>>(livroAssuntosEntity);
        }

        public async Task<LivroAssuntoDTO> GetById(int livroId, int assuntoId)
        {
            var livroAssuntoEntity = await _livroAssuntoRepository.GetByIdAsync(livroId, assuntoId);
            return _mapper.Map<LivroAssuntoDTO>(livroAssuntoEntity);
        }

        public async Task Add(LivroAssuntoDTO livroAssuntoDto)
        {
            var livroAssuntoEntity = _mapper.Map<LivroAssunto>(livroAssuntoDto);
            await _livroAssuntoRepository.CreateAsync(livroAssuntoEntity);
        }

        public async Task Update(LivroAssuntoDTO livroAssuntoDto)
        {
            var livroAssuntoEntity = _mapper.Map<LivroAssunto>(livroAssuntoDto);
            await _livroAssuntoRepository.UpdateAsync(livroAssuntoEntity);
        }

        public async Task Remove(int livroId, int assuntoId)
        {
            var livroAssuntoEntity = await _livroAssuntoRepository.GetByIdAsync(livroId, assuntoId);
            await _livroAssuntoRepository.RemoveAsync(livroAssuntoEntity);
        }
    }
}

