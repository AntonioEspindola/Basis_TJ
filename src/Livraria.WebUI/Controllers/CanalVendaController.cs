using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Livraria.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.WebUI.Controllers
{
    [Route("[controller]/[action]")]
    public class CanalVendaController : Controller
    {
        private readonly ICanalVendaService _canalVendaService;
        private readonly ILogger<CanalVendaController> _logger;

        public CanalVendaController(ICanalVendaService canalVendaService, ILogger<CanalVendaController> logger)
        {
            _canalVendaService = canalVendaService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var canalVenda = await _canalVendaService.GetCanalVendas();
                return View(canalVenda);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter a lista de canais de venda.");
                TempData["ErrorMessage"] = "Erro ao obter a lista de canais de venda. Tente novamente mais tarde.";
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Ocorreu um erro inesperado ao tentar obter a lista de canais de venda. Por favor, tente novamente mais tarde."
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
        public async Task<IActionResult> Create(CanalVendaDTO canalVenda)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _canalVendaService.Add(canalVenda);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao criar um novo canal de venda.");
                    TempData["ErrorMessage"] = "Erro ao criar o canal de venda. Tente novamente mais tarde.";
                    var errorModel = new ErrorViewModel
                    {
                        ErrorMessage = "Ocorreu um erro inesperado ao tentar criar o canal de venda. Por favor, tente novamente mais tarde."
                    };
                    return View("Error", errorModel);
                }
            }
            return View(canalVenda);
        }

        [HttpGet("Alterar")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var canalVendaDto = await _canalVendaService.GetById(id.Value);
                if (canalVendaDto == null)
                    return NotFound();
                return View(canalVendaDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter o canal de venda para edição com ID {Id}.", id);
                TempData["ErrorMessage"] = "Erro ao obter o canal de venda para edição. Tente novamente mais tarde.";
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Ocorreu um erro inesperado ao tentar obter o canal de venda para edição. Por favor, tente novamente mais tarde."
                };
                return View("Error", errorModel);
            }
        }

        [HttpPost("Alterar")]
        public async Task<IActionResult> Edit(CanalVendaDTO canalVendaDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _canalVendaService.Update(canalVendaDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao atualizar o canal de venda com ID {Id}.", canalVendaDto.Id);
                    TempData["ErrorMessage"] = "Erro ao atualizar o canal de venda. Tente novamente mais tarde.";
                    var errorModel = new ErrorViewModel
                    {
                        ErrorMessage = "Ocorreu um erro inesperado ao tentar atualizar o canal de venda. Por favor, tente novamente mais tarde."
                    };
                    return View("Error", errorModel);
                }
            }
            return View(canalVendaDto);
        }

        [HttpGet("Excluir")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var canalVendaDto = await _canalVendaService.GetById(id.Value);
                if (canalVendaDto == null)
                    return NotFound();
                return View(canalVendaDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter o canal de venda para exclusão com ID {Id}.", id);
                TempData["ErrorMessage"] = "Erro ao obter o canal de venda para exclusão. Tente novamente mais tarde.";
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Ocorreu um erro inesperado ao tentar obter o canal de venda para exclusão. Por favor, tente novamente mais tarde."
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
                await _canalVendaService.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir o canal de venda com ID {Id}.", id);
                TempData["ErrorMessage"] = "Erro ao excluir o canal de venda. Tente novamente mais tarde.";
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Ocorreu um erro inesperado ao tentar excluir o canal de venda. Por favor, tente novamente mais tarde."
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
                var canalVendaDto = await _canalVendaService.GetById(id.Value);
                if (canalVendaDto == null)
                    return NotFound();
                return View(canalVendaDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter os detalhes do canal de venda com ID {Id}.", id);
                TempData["ErrorMessage"] = "Erro ao obter os detalhes do canal de venda. Tente novamente mais tarde.";
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Ocorreu um erro inesperado ao tentar obter os detalhes do canal de venda. Por favor, tente novamente mais tarde."
                };
                return View("Error", errorModel);
            }
        }
    }
}
