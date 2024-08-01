using Livraria.Application.DTOs;

namespace Livraria.Application.Interfaces
{
    public interface ILivroAutorService
    {
        Task<IEnumerable<LivroAutorDTO>> GetLivroAutores();
        Task<LivroAutorDTO> GetById(int livroId, int autorId);
        Task Add(LivroAutorDTO livroAutorDto);
        Task Update(LivroAutorDTO livroAutorDto);
        Task Remove(int livroId, int autorId);
    }
}

