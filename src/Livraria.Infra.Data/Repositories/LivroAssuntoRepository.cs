using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using Livraria.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Livraria.Infra.Data.Repositories
{
    public class LivroAssuntoRepository : ILivroAssuntoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LivroAssuntoRepository> _logger;

        public LivroAssuntoRepository(ApplicationDbContext context, ILogger<LivroAssuntoRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<LivroAssunto> CreateAsync(LivroAssunto livroAssunto)
        {
            try
            {
                _context.Add(livroAssunto);
                await _context.SaveChangesAsync();
                return livroAssunto;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erro ao tentar salvar o LivroAssunto no banco de dados.");
                throw new Exception("Ocorreu um erro ao tentar salvar o LivroAssunto no banco de dados. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao tentar salvar o LivroAssunto.");
                throw new Exception("Ocorreu um erro inesperado ao tentar salvar o LivroAssunto. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<LivroAssunto> GetByIdAsync(int livroId, int assuntoId)
        {
            try
            {
                return await _context.LivroAssuntos
                    .SingleOrDefaultAsync(la => la.Livro_Codl == livroId && la.Assunto_CodAs == assuntoId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar recuperar o LivroAssunto com LivroId {LivroId} e AssuntoId {AssuntoId}.", livroId, assuntoId);
                throw new Exception("Ocorreu um erro ao tentar recuperar o LivroAssunto. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<IEnumerable<LivroAssunto>> GetLivroAssuntosAsync()
        {
            try
            {
                return await _context.LivroAssuntos.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar listar os LivroAssuntos.");
                throw new Exception("Ocorreu um erro ao tentar listar os LivroAssuntos. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<LivroAssunto> RemoveAsync(LivroAssunto livroAssunto)
        {
            try
            {
                _context.Remove(livroAssunto);
                await _context.SaveChangesAsync();
                return livroAssunto;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erro ao tentar remover o LivroAssunto com LivroId {LivroId} e AssuntoId {AssuntoId}.", livroAssunto.Livro_Codl, livroAssunto.Assunto_CodAs);
                throw new Exception("Ocorreu um erro ao tentar remover o LivroAssunto do banco de dados. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao tentar remover o LivroAssunto com LivroId {LivroId} e AssuntoId {AssuntoId}.", livroAssunto.Livro_Codl, livroAssunto.Assunto_CodAs);
                throw new Exception("Ocorreu um erro inesperado ao tentar remover o LivroAssunto. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<LivroAssunto> UpdateAsync(LivroAssunto livroAssunto)
        {
            try
            {
                _context.Update(livroAssunto);
                await _context.SaveChangesAsync();
                return livroAssunto;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erro ao tentar atualizar o LivroAssunto com LivroId {LivroId} e AssuntoId {AssuntoId}.", livroAssunto.Livro_Codl, livroAssunto.Assunto_CodAs);
                throw new Exception("Ocorreu um erro ao tentar atualizar o LivroAssunto no banco de dados. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao tentar atualizar o LivroAssunto com LivroId {LivroId} e AssuntoId {AssuntoId}.", livroAssunto.Livro_Codl, livroAssunto.Assunto_CodAs);
                throw new Exception("Ocorreu um erro inesperado ao tentar atualizar o LivroAssunto. Por favor, tente novamente mais tarde.", ex);
            }
        }
    }
}
