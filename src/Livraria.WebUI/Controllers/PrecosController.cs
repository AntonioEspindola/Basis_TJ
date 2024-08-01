using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.WebUI.Controllers
{
    [Route("[controller]/[action]")]
    public class PrecosController : Controller
    {
        private readonly ILivroService _livroService;
        private readonly ILogger<LivrosController> _logger;

        public PrecosController(ILivroService livroService, ILogger<LivrosController> logger)
        {
            _livroService = livroService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var livros = await _livroService.GetLivros();
                return View(livros);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter a lista de livros.");
                TempData["ErrorMessage"] = "Erro ao obter a lista de livros. Tente novamente mais tarde.";
                return View("Error");
            }
        }

        [HttpGet("Detalhes")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var livroDto = await _livroService.GetById(id.Value);
                if (livroDto == null)
                    return NotFound();
                return View(livroDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter os detalhes do livro com ID {Id}.", id);
                TempData["ErrorMessage"] = "Erro ao obter os detalhes do livro. Tente novamente mais tarde.";
                return View("Error");
            }
        }

    }
}


