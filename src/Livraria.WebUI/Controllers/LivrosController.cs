using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.WebUI.Controllers
{
    [Route("[controller]/[action]")]
    public class LivrosController : Controller
    {
        private readonly ILivroService _livroService;
        private readonly ILogger<LivrosController> _logger;

        public LivrosController(ILivroService livroService, ILogger<LivrosController> logger)
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LivroDTO livro)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _livroService.Add(livro);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao criar um novo livro.");
                    TempData["ErrorMessage"] = "Erro ao criar o livro. Tente novamente mais tarde.";
                }
            }
            return View(livro);
        }

        [HttpGet("Alterar")]
        public async Task<IActionResult> Edit(int? id)
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
                _logger.LogError(ex, "Erro ao obter o livro para edição com ID {Id}.", id);
                TempData["ErrorMessage"] = "Erro ao obter o livro para edição. Tente novamente mais tarde.";
                return View("Error");
            }
        }

        [HttpPost("Alterar")]
        public async Task<IActionResult> Edit(LivroDTO livroDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _livroService.Update(livroDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao atualizar o livro com ID {Id}.", livroDto.Id);
                    TempData["ErrorMessage"] = "Erro ao atualizar o livro. Tente novamente mais tarde.";
                }
            }
            return View(livroDto);
        }

        [HttpGet("Excluir")]
        public async Task<IActionResult> Delete(int? id)
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
                _logger.LogError(ex, "Erro ao obter o livro para exclusão com ID {Id}.", id);
                TempData["ErrorMessage"] = "Erro ao obter o livro para exclusão. Tente novamente mais tarde.";
                return View("Error");
            }
        }

        [HttpPost("Excluir")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _livroService.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir o livro com ID {Id}.", id);
                TempData["ErrorMessage"] = "Erro ao excluir o livro. Tente novamente mais tarde.";
                return RedirectToAction(nameof(Delete), new { id });
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

