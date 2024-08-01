using Livraria.API.Models.Erros;
using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatoriosController : ControllerBase
    {
        private readonly ILivroAutorAssuntoService _livroAutorAssuntoService;
        private readonly ILogger<RelatoriosController> _logger;

        public RelatoriosController(ILivroAutorAssuntoService livroAutorAssuntoService, ILogger<RelatoriosController> logger)
        {
            _livroAutorAssuntoService = livroAutorAssuntoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivroAutorAssuntoDTO>>> GetRelatorioLivros()
        {
            try
            {
                var livroAutorAssunto = await _livroAutorAssuntoService.GetRelatorioLivros();
                if (livroAutorAssunto == null || !livroAutorAssunto.Any())
                {
                    return NotFound(new ErrorResponse { Message = "Dados não encontrados para geração do Relatório de Livros." });
                }
                return Ok(livroAutorAssunto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter o relatório de livros.");
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }
    }
}


