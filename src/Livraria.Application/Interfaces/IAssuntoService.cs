using Livraria.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Application.Interfaces
{
    public interface IAssuntoService
    {
        Task<IEnumerable<AssuntoDTO>> GetAssuntos();
        Task<AssuntoDTO> GetById(int? id);
        Task Add(AssuntoDTO assuntoDto);
        Task Update(AssuntoDTO assuntoDto);
        Task Remove(int? id);
    }
}

