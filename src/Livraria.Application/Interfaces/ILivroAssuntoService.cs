using Livraria.Application.DTOs;

namespace Livraria.Application.Interfaces
{
    public interface ILivroAssuntoService
    {
        Task<IEnumerable<LivroAssuntoDTO>> GetLivroAssuntos();
        Task<LivroAssuntoDTO> GetById(int livroId, int assuntoId);
        Task Add(LivroAssuntoDTO livroAssuntoDto);
        Task Update(LivroAssuntoDTO livroAssuntoDto);
        Task Remove(int livroId, int assuntoId);
    }
}

