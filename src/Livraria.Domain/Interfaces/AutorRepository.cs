using Livraria.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Domain.Interfaces
{
    public interface IAutorRepository
    {
        Task<IEnumerable<Autor>> GetAutoresAsync();
        Task<Autor> GetByIdAsync(int? id);

        Task<Autor> CreateAsync(Autor autor);
        Task<Autor> UpdateAsync(Autor autor);
        Task<Autor> RemoveAsync(Autor autor);
    }
}

