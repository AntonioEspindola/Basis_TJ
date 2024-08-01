using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using Livraria.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Livraria.Infra.Data.Repositories
{
    public class LivroPrecoCanalVendaRepository : ILivroPrecoCanalVendaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LivroPrecoCanalVendaRepository> _logger;

        public LivroPrecoCanalVendaRepository(ApplicationDbContext context, ILogger<LivroPrecoCanalVendaRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<LivroPrecoCanalVenda> CreateAsync(LivroPrecoCanalVenda livroPrecoCanalVenda)
        {
            try
            {
                _context.Add(livroPrecoCanalVenda);
                await _context.SaveChangesAsync();
                return livroPrecoCanalVenda;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erro ao tentar salvar LivroPrecoCanalVenda no banco de dados.");
                throw new Exception("Ocorreu um erro ao tentar salvar LivroPrecoCanalVenda no banco de dados. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao tentar salvar LivroPrecoCanalVenda.");
                throw new Exception("Ocorreu um erro inesperado ao tentar salvar LivroPrecoCanalVenda. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<LivroPrecoCanalVenda> GetByIdAsync(int livroId, int canalVendaId)
        {
            try
            {
                return await _context.LivroPrecoCanalVenda
                    .SingleOrDefaultAsync(lpcv => lpcv.Livro_Codl == livroId && lpcv.CanalVenda_CodCanal == canalVendaId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar recuperar LivroPrecoCanalVenda com LivroId {LivroId} e CanalVendaId {CanalVendaId}.", livroId, canalVendaId);
                throw new Exception("Ocorreu um erro ao tentar recuperar LivroPrecoCanalVenda. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<IEnumerable<LivroPrecoCanalVenda>> GetLivroPrecoCanalVendaAsync()
        {
            try
            {
                return await _context.LivroPrecoCanalVenda.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar listar os LivroPrecoCanalVenda.");
                throw new Exception("Ocorreu um erro ao tentar listar os LivroPrecoCanalVenda. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<LivroPrecoCanalVenda> RemoveAsync(LivroPrecoCanalVenda livroPrecoCanalVenda)
        {
            try
            {
                _context.Remove(livroPrecoCanalVenda);
                await _context.SaveChangesAsync();
                return livroPrecoCanalVenda;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erro ao tentar remover LivroPrecoCanalVenda com LivroId {LivroId} e CanalVendaId {CanalVendaId}.", livroPrecoCanalVenda.Livro_Codl, livroPrecoCanalVenda.CanalVenda_CodCanal);
                throw new Exception("Ocorreu um erro ao tentar remover LivroPrecoCanalVenda do banco de dados. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao tentar remover LivroPrecoCanalVenda com LivroId {LivroId} e CanalVendaId {CanalVendaId}.", livroPrecoCanalVenda.Livro_Codl, livroPrecoCanalVenda.CanalVenda_CodCanal);
                throw new Exception("Ocorreu um erro inesperado ao tentar remover LivroPrecoCanalVenda. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<LivroPrecoCanalVenda> UpdateAsync(LivroPrecoCanalVenda livroPrecoCanalVenda)
        {
            try
            {
                _context.Update(livroPrecoCanalVenda);
                await _context.SaveChangesAsync();
                return livroPrecoCanalVenda;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erro ao tentar atualizar LivroPrecoCanalVenda com LivroId {LivroId} e CanalVendaId {CanalVendaId} no banco de dados.", livroPrecoCanalVenda.Livro_Codl, livroPrecoCanalVenda.CanalVenda_CodCanal);
                throw new Exception("Ocorreu um erro ao tentar atualizar LivroPrecoCanalVenda no banco de dados. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao tentar atualizar LivroPrecoCanalVenda com LivroId {LivroId} e CanalVendaId {CanalVendaId}.", livroPrecoCanalVenda.Livro_Codl, livroPrecoCanalVenda.CanalVenda_CodCanal);
                throw new Exception("Ocorreu um erro inesperado ao tentar atualizar LivroPrecoCanalVenda. Por favor, tente novamente mais tarde.", ex);
            }
        }
    }
}
