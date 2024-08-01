using Livraria.Application.DTOs;

namespace Livraria.Application.Interfaces
{
    public interface ILivroAutorAssuntoService
    {
        Task<IEnumerable<LivroAutorAssuntoDTO>> GetRelatorioLivros();
        
    }
}
