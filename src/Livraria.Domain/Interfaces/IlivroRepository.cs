using Livraria.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Domain.Interfaces
{
    public interface ILivroRepository
    {
        Task<IEnumerable<Livro>> GetLivrosAsync();
        Task<Livro> GetByIdAsync(int? id);

        Task<Livro> CreateAsync(Livro livro);
        Task<Livro> UpdateAsync(Livro livro);
        Task<Livro> RemoveAsync(Livro livro);
    }
}

