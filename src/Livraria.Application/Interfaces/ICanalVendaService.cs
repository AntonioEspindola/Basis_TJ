using Livraria.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Application.Interfaces
{
    public interface ICanalVendaService
    {
        Task<IEnumerable<CanalVendaDTO>> GetCanalVendas();
        Task<CanalVendaDTO> GetById(int id);
        Task Add(CanalVendaDTO canalVendaDto);
        Task Update(CanalVendaDTO canalVendaDto);
        Task Remove(int id);
    }
}

