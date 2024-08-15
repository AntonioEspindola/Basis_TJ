using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using Livraria.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Livraria.Infra.Data.Repositories
{
    public class LivroAutorRepository : ILivroAutorRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LivroAutorRepository> _logger;

        public LivroAutorRepository(ApplicationDbContext context, ILogger<LivroAutorRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<LivroAutor> CreateAsync(LivroAutor livroAutor)
        {
            try
            {
                _context.Add(livroAutor);
                await _context.SaveChangesAsync();
                return livroAutor;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao criar o LivroAutor.");
                throw new Exception("Ocorreu um erro ao criar o LivroAutor. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro inesperado.");
                throw new Exception("Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<LivroAutor> GetByIdAsync(int livroId, int autorId)
        {
            try
            {
                return await _context.LivroAutores
                    .SingleOrDefaultAsync(la => la.Livro_Codl == livroId && la.Autor_CodAu == autorId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao recuperar o LivroAutor.");
                throw new Exception("Ocorreu um erro ao recuperar o LivroAutor. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<IEnumerable<LivroAutor>> GetLivroAutoresAsync()
        {
            try
            {
                return await _context.LivroAutores.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao recuperar a lista de LivroAutores.");
                throw new Exception("Ocorreu um erro ao recuperar a lista de LivroAutores. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<LivroAutor> RemoveAsync(LivroAutor livroAutor)
        {
            try
            {
                _context.Remove(livroAutor);
                await _context.SaveChangesAsync();
                return livroAutor;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao remover o LivroAutor.");
                throw new Exception("Ocorreu um erro ao remover o LivroAutor. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro inesperado.");
                throw new Exception("Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<LivroAutor> UpdateAsync(LivroAutor livroAutor)
        {
            try
            {
                _context.Update(livroAutor);
                await _context.SaveChangesAsync();
                return livroAutor;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao atualizar o LivroAutor.");
                throw new Exception("Ocorreu um erro ao atualizar o LivroAutor. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro inesperado.");
                throw new Exception("Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task AddRangeAsync(IEnumerable<LivroAutor> livroAutor)
        {
            try
            {
                _context.AddRange(livroAutor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erro ao tentar inserir Livro x Autor no banco de dados.");
                throw new Exception("Ocorreu um erro ao tentar inserir Livro x Autor no banco de dados. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao tentar inserir Livro x Autor no banco de dados.");
                throw new Exception("Ocorreu um erro inesperado ao tentar inserir Livro x Autor no banco de dados. Por favor, tente novamente mais tarde.", ex);
            }
        }
    }
}


