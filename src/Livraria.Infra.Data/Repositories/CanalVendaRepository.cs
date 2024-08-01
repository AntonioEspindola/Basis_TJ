using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using Livraria.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Infra.Data.Repositories
{
    public class CanalVendaRepository : ICanalVendaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CanalVendaRepository> _logger;

        public CanalVendaRepository(ApplicationDbContext context, ILogger<CanalVendaRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<CanalVenda> CreateAsync(CanalVenda canalVenda)
        {
            try
            {
                _context.Add(canalVenda);
                await _context.SaveChangesAsync();
                return canalVenda;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erro ao tentar salvar o CanalVenda no banco de dados.");
                throw new Exception("Ocorreu um erro ao tentar salvar o CanalVenda no banco de dados. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao tentar salvar o CanalVenda.");
                throw new Exception("Ocorreu um erro inesperado ao tentar salvar o CanalVenda. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<CanalVenda> GetByIdAsync(int id)
        {
            try
            {
                return await _context.CanalVenda.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar recuperar o CanalVenda com ID {Id}.", id);
                throw new Exception("Ocorreu um erro ao tentar recuperar o CanalVenda. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<IEnumerable<CanalVenda>> GetCanalVendaAsync()
        {
            try
            {
                return await _context.CanalVenda.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar listar os CanalVendas.");
                throw new Exception("Ocorreu um erro ao tentar listar os CanalVendas. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<CanalVenda> RemoveAsync(CanalVenda canalVenda)
        {
            try
            {
                _context.Remove(canalVenda);
                await _context.SaveChangesAsync();
                return canalVenda;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erro ao tentar remover o CanalVenda com ID {Id}.", canalVenda.Id);
                throw new Exception("Ocorreu um erro ao tentar remover o CanalVenda do banco de dados. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao tentar remover o CanalVenda com ID {Id}.", canalVenda.Id);
                throw new Exception("Ocorreu um erro inesperado ao tentar remover o CanalVenda. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<CanalVenda> UpdateAsync(CanalVenda canalVenda)
        {
            try
            {
                _context.Update(canalVenda);
                await _context.SaveChangesAsync();
                return canalVenda;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erro ao tentar atualizar o CanalVenda com ID {Id} no banco de dados.", canalVenda.Id);
                throw new Exception("Ocorreu um erro ao tentar atualizar o CanalVenda no banco de dados. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao tentar atualizar o CanalVenda com ID {Id}.", canalVenda.Id);
                throw new Exception("Ocorreu um erro inesperado ao tentar atualizar o CanalVenda. Por favor, tente novamente mais tarde.", ex);
            }
        }
    }
}


