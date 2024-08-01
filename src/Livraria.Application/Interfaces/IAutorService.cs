using Livraria.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Application.Interfaces
{
    public interface IAutorService
    {
        Task<IEnumerable<AutorDTO>> GetAutores();
        Task<AutorDTO> GetById(int? id);
        Task Add(AutorDTO autorDto);
        Task Update(AutorDTO autorDto);
        Task Remove(int? id);
    }
}

