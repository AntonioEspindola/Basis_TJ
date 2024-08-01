using Livraria.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Application.Interfaces
{
    public interface ILivroService
    {
        Task<IEnumerable<LivroDTO>> GetLivros();
        Task<LivroDTO> GetById(int? id);
        Task Add(LivroDTO livroDto);
        Task Update(LivroDTO livroDto);
        Task Remove(int? id);
    }
}

