using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Livraria.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Ocorreu um erro inesperado ao tentar obter a lista de livros. Por favor, tente novamente mais tarde."
                };
                return View("Error", errorModel);
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
                    var errorModel = new ErrorViewModel
                    {
                        ErrorMessage = "Erro ao criar o livro. Por favor, tente novamente mais tarde."
                    };
                    return View("Error", errorModel);
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
                _logger.LogError(ex, "Erro ao obter o livro para edição.");
                TempData["ErrorMessage"] = "Erro ao obter o livro para edição. Tente novamente mais tarde.";
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Erro ao obter o livro para edição. Por favor, tente novamente mais tarde."
                };
                return View("Error", errorModel);
            }
        }

        [HttpPost("Alterar")]
        public async Task<IActionResult> Edit(LivroDTO livroDto, string livroAutores, string livroAssuntos, string livroPrecoCanalVenda)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrEmpty(livroAutores))
                    {
                        livroDto.LivroAutores = JsonSerializer.Deserialize<ICollection<LivroAutorDTO>>(livroAutores);
                    }

                    if (!string.IsNullOrEmpty(livroAssuntos))
                    {
                        livroDto.LivroAssuntos = JsonSerializer.Deserialize<ICollection<LivroAssuntoDTO>>(livroAssuntos);
                    }

                    if (!string.IsNullOrEmpty(livroPrecoCanalVenda))
                    {
                        livroDto.LivroPrecoCanalVenda = JsonSerializer.Deserialize<ICollection<LivroPrecoCanalVendaDTO>>(livroPrecoCanalVenda);
                    }

                    await _livroService.Update(livroDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao atualizar o livro com ID {Id}.", livroDto.Id);
                    TempData["ErrorMessage"] = "Erro ao atualizar o livro. Tente novamente mais tarde.";

                    var errorModel = new ErrorViewModel
                    {
                        ErrorMessage = "Ocorreu um erro inesperado ao tentar atualizar o livro. Por favor, tente novamente mais tarde."
                    };

                    return View("Error", errorModel);
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
                _logger.LogError(ex, "Erro ao obter o livro para exclusão." );
                TempData["ErrorMessage"] = "Erro ao obter o livro para exclusão. Tente novamente mais tarde.";
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Erro ao obter o livro para exclusão. Por favor, tente novamente mais tarde."
                };
                return View("Error", errorModel);
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
                _logger.LogError(ex, "Erro ao excluir o livro com ID"+  id );
                TempData["ErrorMessage"] = "Erro ao excluir o livro. Tente novamente mais tarde.";
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Erro ao excluir o livro. Por favor, tente novamente mais tarde."
                };
                return View("Error", errorModel);
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
                _logger.LogError(ex, "Erro ao obter os detalhes do livro com ID" + id );
                TempData["ErrorMessage"] = "Erro ao obter os detalhes do livro. Tente novamente mais tarde.";
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Erro ao obter os detalhes do livro. Por favor, tente novamente mais tarde."
                };
                return View("Error", errorModel);
            }
        }
    }
}

