using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using Livraria.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Livraria.Infra.Data.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AutorRepository> _logger;

        public AutorRepository(ApplicationDbContext context, ILogger<AutorRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Autor> CreateAsync(Autor autor)
        {
            try
            {
                _context.Add(autor);
                await _context.SaveChangesAsync();
                return autor;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erro ao tentar salvar o autor no banco de dados.");
                throw new Exception("Ocorreu um erro ao tentar salvar o autor no banco de dados. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao tentar salvar o autor.");
                throw new Exception("Ocorreu um erro inesperado ao tentar salvar o autor. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<Autor> GetByIdAsync(int? id)
        {
            try
            {
                return await _context.Autor
                    //.Include(a => a.LivroAutores).ThenInclude(la => la.Livro)
                    .SingleOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar recuperar o autor com ID {Id}.", id);
                throw new Exception("Ocorreu um erro ao tentar recuperar o autor. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<IEnumerable<Autor>> GetAutoresAsync()
        {
            try
            {
                return await _context.Autor.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar listar os autores.");
                throw new Exception("Ocorreu um erro ao tentar listar os autores. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<Autor> RemoveAsync(Autor autor)
        {
            try
            {
                _context.Remove(autor);
                await _context.SaveChangesAsync();
                return autor;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erro ao tentar remover o autor com ID {Id} do banco de dados.", autor.Id);
                throw new Exception("Ocorreu um erro ao tentar remover o autor do banco de dados. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao tentar remover o autor com ID {Id}.", autor.Id);
                throw new Exception("Ocorreu um erro inesperado ao tentar remover o autor. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<Autor> UpdateAsync(Autor autor)
        {
            try
            {
                _context.Update(autor);
                await _context.SaveChangesAsync();
                return autor;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erro ao tentar atualizar o autor com ID {Id} no banco de dados.", autor.Id);
                throw new Exception("Ocorreu um erro ao tentar atualizar o autor no banco de dados. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao tentar atualizar o autor com ID {Id}.", autor.Id);
                throw new Exception("Ocorreu um erro inesperado ao tentar atualizar o autor. Por favor, tente novamente mais tarde.", ex);
            }
        }
    }
}


