using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using Livraria.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Data.Repositories
{
    public class AssuntoRepository : IAssuntoRepository
    {
        private readonly ApplicationDbContext _context;

        public AssuntoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Assunto> CreateAsync(Assunto assunto)
        {
            try
            {
                _context.Add(assunto);
                await _context.SaveChangesAsync();
                return assunto;
            }
            catch (DbUpdateException ex)
            {
                // Log the exception details (not implemented here)
                throw new Exception("Ocorreu um erro ao tentar salvar o assunto no banco de dados. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                // Log the exception details (not implemented here)
                throw new Exception("Ocorreu um erro inesperado ao tentar salvar o assunto. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<Assunto> GetByIdAsync(int? id)
        {
            try
            {
                return await _context.Assunto
                    .Include(a => a.LivroAssuntos).ThenInclude(la => la.Livro)
                    .SingleOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception ex)
            {
                // Log the exception details (not implemented here)
                throw new Exception("Ocorreu um erro ao tentar recuperar o assunto. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<IEnumerable<Assunto>> GetAssuntosAsync()
        {
            try
            {
                return await _context.Assunto.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception details (not implemented here)
                throw new Exception("Ocorreu um erro ao tentar listar os assuntos. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<Assunto> RemoveAsync(Assunto assunto)
        {
            try
            {
                _context.Remove(assunto);
                await _context.SaveChangesAsync();
                return assunto;
            }
            catch (DbUpdateException ex)
            {
                // Log the exception details (not implemented here)
                throw new Exception("Ocorreu um erro ao tentar remover o assunto do banco de dados. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                // Log the exception details (not implemented here)
                throw new Exception("Ocorreu um erro inesperado ao tentar remover o assunto. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<Assunto> UpdateAsync(Assunto assunto)
        {
            try
            {
                _context.Update(assunto);
                await _context.SaveChangesAsync();
                return assunto;
            }
            catch (DbUpdateException ex)
            {
                // Log the exception details (not implemented here)
                throw new Exception("Ocorreu um erro ao tentar atualizar o assunto no banco de dados. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                // Log the exception details (not implemented here)
                throw new Exception("Ocorreu um erro inesperado ao tentar atualizar o assunto. Por favor, tente novamente mais tarde.", ex);
            }
        }
    }
}


