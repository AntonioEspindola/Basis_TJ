using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using Livraria.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Livraria.Infra.Data.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LivroRepository> _logger;

        public LivroRepository(ApplicationDbContext context, ILogger<LivroRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Livro> CreateAsync(Livro livro)
        {
            try
            {
                _context.Add(livro);
                await _context.SaveChangesAsync();
                return livro;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erro ao tentar salvar o Livro no banco de dados.");
                throw new Exception("Ocorreu um erro ao tentar salvar o Livro no banco de dados. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao tentar salvar o Livro.");
                throw new Exception("Ocorreu um erro inesperado ao tentar salvar o Livro. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<Livro> GetByIdAsync(int? id)
        {
            try
            {
                return await _context.Livro
                    .Include(l => l.LivroAutores).ThenInclude(la => la.Autor)
                    .Include(l => l.LivroAssuntos).ThenInclude(la => la.Assunto)
                    .Include(l => l.LivroPrecoCanalVenda).ThenInclude(lp => lp.CanalVenda)
                    .SingleOrDefaultAsync(l => l.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar recuperar o Livro com ID {Id}.", id);
                throw new Exception("Ocorreu um erro ao tentar recuperar o Livro. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<IEnumerable<Livro>> GetLivrosAsync()
        {
            try
            {
                return await _context.Livro.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar listar os Livros.");
                throw new Exception("Ocorreu um erro ao tentar listar os Livros. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<Livro> RemoveAsync(Livro livro)
        {
            try
            {
                _context.Remove(livro);
                await _context.SaveChangesAsync();
                return livro;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erro ao tentar remover o Livro com ID {Id}.", livro.Id);
                throw new Exception("Ocorreu um erro ao tentar remover o Livro do banco de dados. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao tentar remover o Livro com ID {Id}.", livro.Id);
                throw new Exception("Ocorreu um erro inesperado ao tentar remover o Livro. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<Livro> UpdateAsync(Livro livro)
        {
            try
            {
                _context.Update(livro);
                await _context.SaveChangesAsync();
                return livro;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erro ao tentar atualizar o Livro com ID {Id} no banco de dados.", livro.Id);
                throw new Exception("Ocorreu um erro ao tentar atualizar o Livro no banco de dados. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao tentar atualizar o Livro com ID {Id}.", livro.Id);
                throw new Exception("Ocorreu um erro inesperado ao tentar atualizar o Livro. Por favor, tente novamente mais tarde.", ex);
            }
        }
    }
}


