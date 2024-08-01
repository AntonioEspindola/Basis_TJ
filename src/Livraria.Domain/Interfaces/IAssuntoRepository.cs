using Livraria.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Domain.Interfaces
{
    public interface IAssuntoRepository
    {
        Task<IEnumerable<Assunto>> GetAssuntosAsync();
        Task<Assunto> GetByIdAsync(int? id);

        Task<Assunto> CreateAsync(Assunto assunto);
        Task<Assunto> UpdateAsync(Assunto assunto);
        Task<Assunto> RemoveAsync(Assunto assunto);
    }
}

