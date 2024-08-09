using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Livraria.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.WebUI.Controllers
{
    [Route("[controller]/[action]")]
    public class AutoresController : Controller
    {
        private readonly IAutorService _autorService;
        private readonly ILogger<AutoresController> _logger;

        public AutoresController(IAutorService autorService, ILogger<AutoresController> logger)
        {
            _autorService = autorService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var autores = await _autorService.GetAutores();
                return View(autores);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter a lista de autores.");
                TempData["ErrorMessage"] = "Erro ao obter a lista de autores. Tente novamente mais tarde.";
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Ocorreu um erro inesperado ao tentar obter a lista de autores. Por favor, tente novamente mais tarde."
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
        public async Task<IActionResult> Create(AutorDTO autor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _autorService.Add(autor);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao criar um novo autor.");
                    TempData["ErrorMessage"] = "Erro ao criar o autor. Tente novamente mais tarde.";
                    var errorModel = new ErrorViewModel
                    {
                        ErrorMessage = "Ocorreu um erro inesperado ao tentar criar o autor. Por favor, tente novamente mais tarde."
                    };
                    return View("Error", errorModel);
                }
            }
            return View(autor);
        }

        [HttpGet("Alterar")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var autorDto = await _autorService.GetById(id.Value);
                if (autorDto == null)
                    return NotFound();
                return View(autorDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter o autor para edição com ID {Id}.", id);
                TempData["ErrorMessage"] = "Erro ao obter o autor para edição. Tente novamente mais tarde.";
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Ocorreu um erro inesperado ao tentar obter o autor para edição. Por favor, tente novamente mais tarde."
                };
                return View("Error", errorModel);
            }
        }

        [HttpPost("Alterar")]
        public async Task<IActionResult> Edit(AutorDTO autorDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _autorService.Update(autorDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao atualizar o autor com ID {Id}.", autorDto.Id);
                    TempData["ErrorMessage"] = "Erro ao atualizar o autor. Tente novamente mais tarde.";
                    var errorModel = new ErrorViewModel
                    {
                        ErrorMessage = "Ocorreu um erro inesperado ao tentar atualizar o autor. Por favor, tente novamente mais tarde."
                    };
                    return View("Error", errorModel);
                }
            }
            return View(autorDto);
        }

        [HttpGet("Excluir")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var autorDto = await _autorService.GetById(id.Value);
                if (autorDto == null)
                    return NotFound();
                return View(autorDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter o autor para exclusão com ID {Id}.", id);
                TempData["ErrorMessage"] = "Erro ao obter o autor para exclusão. Tente novamente mais tarde.";
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Ocorreu um erro inesperado ao tentar obter o autor para exclusão. Por favor, tente novamente mais tarde."
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
                await _autorService.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir o autor com ID {Id}.", id);
                TempData["ErrorMessage"] = "Erro ao excluir o autor. Tente novamente mais tarde.";
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Ocorreu um erro inesperado ao tentar excluir o autor. Por favor, tente novamente mais tarde."
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
                var autorDto = await _autorService.GetById(id.Value);
                if (autorDto == null)
                    return NotFound();
                return View(autorDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter os detalhes do autor com ID {Id}.", id);
                TempData["ErrorMessage"] = "Erro ao obter os detalhes do autor. Tente novamente mais tarde.";
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Ocorreu um erro inesperado ao tentar obter os detalhes do autor. Por favor, tente novamente mais tarde."
                };
                return View("Error", errorModel);
            }
        }
    }
}


