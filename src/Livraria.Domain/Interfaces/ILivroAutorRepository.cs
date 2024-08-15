using Livraria.Domain.Entities;

namespace Livraria.Domain.Interfaces
{
    public interface ILivroAutorRepository
    {
        Task<IEnumerable<LivroAutor>> GetLivroAutoresAsync();
        Task<LivroAutor> GetByIdAsync(int livroId, int autorId);

        Task<LivroAutor> CreateAsync(LivroAutor livroAssunto);
        Task<LivroAutor> UpdateAsync(LivroAutor livroAssunto);
        Task<LivroAutor> RemoveAsync(LivroAutor livroAssunto);
        Task AddRangeAsync(IEnumerable<LivroAutor> livroAutor);
    }
}
