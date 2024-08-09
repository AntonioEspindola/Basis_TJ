using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Livraria.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.WebUI.Controllers
{
    [Route("[controller]/[action]")]
    public class AssuntosController : Controller
    {
        private readonly IAssuntoService _assuntoService;
        private readonly ILogger<AssuntosController> _logger;

        public AssuntosController(IAssuntoService assuntoService, ILogger<AssuntosController> logger)
        {
            _assuntoService = assuntoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var assuntos = await _assuntoService.GetAssuntos();
                return View(assuntos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter a lista de assuntos.");
                TempData["ErrorMessage"] = "Erro ao obter a lista de assuntos. Tente novamente mais tarde.";
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Ocorreu um erro ao obter a lista de assuntos. Por favor, tente novamente mais tarde."
                };
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AssuntoDTO assunto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _assuntoService.Add(assunto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao criar um novo assunto.");
                    TempData["ErrorMessage"] = "Erro ao criar o assunto. Tente novamente mais tarde.";
                    var errorModel = new ErrorViewModel
                    {
                        ErrorMessage = "Ocorreu um erro ao obter a lista de assuntos. Por favor, tente novamente mais tarde."
                    };
                    return View("Error");
                }
            }
            return View(assunto);
        }

        [HttpGet("Alterar")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var assuntoDto = await _assuntoService.GetById(id.Value);
                if (assuntoDto == null)
                    return NotFound();
                return View(assuntoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter o assunto para edição com ID {Id}.", id);
                TempData["ErrorMessage"] = "Erro ao obter o assunto para edição. Tente novamente mais tarde.";
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Ocorreu um erro ao obter o assunto para edição. Por favor, tente novamente mais tarde."
                };
                return View("Error");
            }
        }

        [HttpPost("Alterar")]
        public async Task<IActionResult> Edit(AssuntoDTO assuntoDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _assuntoService.Update(assuntoDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao atualizar o assunto com ID {Id}.", assuntoDto.Id);
                    TempData["ErrorMessage"] = "Erro ao atualizar o assunto. Tente novamente mais tarde.";
                    var errorModel = new ErrorViewModel
                    {
                        ErrorMessage = "Ocorreu um erro ao atualizar o assunto. Por favor, tente novamente mais tarde."
                    };
                    return View("Error");
                }
            }
            return View(assuntoDto);
        }

        [HttpGet("Excluir")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var assuntoDto = await _assuntoService.GetById(id.Value);
                if (assuntoDto == null)
                    return NotFound();
                return View(assuntoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter o assunto para exclusão com ID {Id}.", id);
                TempData["ErrorMessage"] = "Erro ao obter o assunto para exclusão. Tente novamente mais tarde.";
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Erro ao obter o assunto para exclusão. Por favor, tente novamente mais tarde."
                };
                return View("Error");
            }
        }

        [HttpPost("Excluir")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _assuntoService.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir o assunto com ID {Id}.", id);
                TempData["ErrorMessage"] = "Erro ao excluir o assunto. Tente novamente mais tarde.";
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Erro ao excluir o assunto. Por favor, tente novamente mais tarde."
                };
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
                var assuntoDto = await _assuntoService.GetById(id.Value);
                if (assuntoDto == null)
                    return NotFound();
                return View(assuntoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter os detalhes do assunto com ID {Id}.", id);
                TempData["ErrorMessage"] = "Erro ao obter os detalhes do assunto. Tente novamente mais tarde.";
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Erro ao obter os detalhes do assunto. Por favor, tente novamente mais tarde."
                };
                return View("Error");
            }
        }
    }
}
