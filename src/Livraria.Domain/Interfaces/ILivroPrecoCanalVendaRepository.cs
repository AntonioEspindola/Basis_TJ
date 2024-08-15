using Livraria.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Domain.Interfaces
{
    public interface ILivroPrecoCanalVendaRepository
    {
        Task<IEnumerable<LivroPrecoCanalVenda>> GetLivroPrecoCanalVendaAsync();
        Task<LivroPrecoCanalVenda> GetByIdAsync(int livroId, int canalVendaId);

        Task<LivroPrecoCanalVenda> CreateAsync(LivroPrecoCanalVenda livroPrecoCanalVenda);

        Task AddRangePrecoCanalVendaAsync(IEnumerable<LivroPrecoCanalVenda> livroPrecoCanalVenda);
        Task<LivroPrecoCanalVenda> UpdateAsync(LivroPrecoCanalVenda livroPrecoCanalVenda);
        Task<LivroPrecoCanalVenda> RemoveAsync(LivroPrecoCanalVenda livroPrecoCanalVenda);
    }
}
