using Livraria.Domain.Entities;

namespace Livraria.Domain.Interfaces
{
    public interface ILivroAssuntoRepository
    {
        Task<IEnumerable<LivroAssunto>> GetLivroAssuntosAsync();
        Task<LivroAssunto> GetByIdAsync(int livroId, int assuntoId);

        Task<LivroAssunto> CreateAsync(LivroAssunto livroAssunto);
        Task<LivroAssunto> UpdateAsync(LivroAssunto livroAssunto);
        Task<LivroAssunto> RemoveAsync(LivroAssunto livroAssunto);
        Task AddRangeAsync(IEnumerable<LivroAssunto> livroAssunto);
    }
}
