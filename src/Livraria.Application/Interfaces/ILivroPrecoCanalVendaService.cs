using Livraria.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Application.Interfaces
{
    public interface ILivroPrecoCanalVendaService
    {
        Task<IEnumerable<LivroPrecoCanalVendaDTO>> GetLivroPrecoCanalVenda();
        Task<LivroPrecoCanalVendaDTO> GetById(int livroId, int canalVendaId);
        Task Add(LivroPrecoCanalVendaDTO livroPrecoCanalVendaDto);
        Task Update(LivroPrecoCanalVendaDTO livroPrecoCanalVendaDto);
        Task Remove(int livroId, int canalVendaId);
    }
}

