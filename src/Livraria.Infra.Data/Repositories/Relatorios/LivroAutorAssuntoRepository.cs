using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using Livraria.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Livraria.Infra.Data.Repositories
{
    public class LivroAutorAssuntoRepository : ILivroAutorAssuntoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LivroAutorAssuntoRepository> _logger;

        public LivroAutorAssuntoRepository(ApplicationDbContext context, ILogger<LivroAutorAssuntoRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<LivroAutorAssunto>> GetRelatorioLivros()
        {
            try
            {
                return await _context.LivroAutorAssuntos.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar listar os LivroAutorAssuntos.");
                throw new Exception("Ocorreu um erro ao tentar listar os LivroAutorAssuntos. Por favor, tente novamente mais tarde.", ex);
            }
        }
    }
}

