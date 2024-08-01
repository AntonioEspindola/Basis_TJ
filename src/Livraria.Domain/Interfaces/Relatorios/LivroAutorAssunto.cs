using Livraria.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Domain.Interfaces
{
    public interface ILivroAutorAssuntoRepository
    {
        Task<IEnumerable<LivroAutorAssunto>> GetRelatorioLivros();
    }
}
