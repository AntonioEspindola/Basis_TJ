using Livraria.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Domain.Interfaces
{
    public interface ICanalVendaRepository
    {
        Task<IEnumerable<CanalVenda>> GetCanalVendaAsync();
        Task<CanalVenda> GetByIdAsync(int id);

        Task<CanalVenda> CreateAsync(CanalVenda canalVenda);
        Task<CanalVenda> UpdateAsync(CanalVenda canalVenda);
        Task<CanalVenda> RemoveAsync(CanalVenda canalVenda);
    }
}

