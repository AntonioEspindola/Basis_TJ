using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Livraria.WebUI.Controllers
{
    [Route("[controller]/[action]")]
    public class LivroPrecoCanalVendaController : Controller
    {
        private readonly ILivroPrecoCanalVendaService _livroPrecoCanalVendaService;
        private readonly ILogger<LivroPrecoCanalVendaController> _logger;

        public LivroPrecoCanalVendaController(ILivroPrecoCanalVendaService livroPrecoCanalVendaService, ILogger<LivroPrecoCanalVendaController> logger)
        {
            _livroPrecoCanalVendaService = livroPrecoCanalVendaService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var livroPrecoCanalVenda = await _livroPrecoCanalVendaService.GetLivroPrecoCanalVenda();
                return View(livroPrecoCanalVenda);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter a lista de preços de livros por canal de venda.");
                TempData["ErrorMessage"] = "Erro ao obter a lista de preços de livros por canal de venda. Tente novamente mais tarde.";
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LivroPrecoCanalVendaDTO livroPrecoCanalVenda)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _livroPrecoCanalVendaService.Add(livroPrecoCanalVenda);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao criar um novo preço de livro por canal de venda.");
                    TempData["ErrorMessage"] = "Erro ao criar o preço do livro por canal de venda. Tente novamente mais tarde.";
                }
            }
            return View(livroPrecoCanalVenda);
        }

        [HttpGet("Alterar")]
        public async Task<IActionResult> Edit(int? livroId, int? canalVendaId)
        {
            if (livroId == null || canalVendaId == null)
                return NotFound();

            try
            {
                var livroPrecoCanalVendaDto = await _livroPrecoCanalVendaService.GetById(livroId.Value, canalVendaId.Value);
                if (livroPrecoCanalVendaDto == null)
                    return NotFound();
                return View(livroPrecoCanalVendaDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter o preço do livro para edição com LivroId {LivroId} e CanalVendaId {CanalVendaId}.", livroId, canalVendaId);
                TempData["ErrorMessage"] = "Erro ao obter o preço do livro para edição. Tente novamente mais tarde.";
                return View("Error");
            }
        }

        [HttpPost("Alterar")]
        public async Task<IActionResult> Edit(LivroPrecoCanalVendaDTO livroPrecoCanalVendaDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _livroPrecoCanalVendaService.Update(livroPrecoCanalVendaDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao atualizar o preço do livro com LivroId {LivroId} e CanalVendaId {CanalVendaId}.", livroPrecoCanalVendaDto.Livro_Codl, livroPrecoCanalVendaDto.CanalVenda_CodCanal);
                    TempData["ErrorMessage"] = "Erro ao atualizar o preço do livro. Tente novamente mais tarde.";
                }
            }
            return View(livroPrecoCanalVendaDto);
        }

        [HttpGet("Excluir")]
        public async Task<IActionResult> Delete(int? livroId, int? canalVendaId)
        {
            if (livroId == null || canalVendaId == null)
                return NotFound();

            try
            {
                var livroPrecoCanalVendaDto = await _livroPrecoCanalVendaService.GetById(livroId.Value, canalVendaId.Value);
                if (livroPrecoCanalVendaDto == null)
                    return NotFound();
                return View(livroPrecoCanalVendaDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter o preço do livro para exclusão com LivroId {LivroId} e CanalVendaId {CanalVendaId}.", livroId, canalVendaId);
                TempData["ErrorMessage"] = "Erro ao obter o preço do livro para exclusão. Tente novamente mais tarde.";
                return View("Error");
            }
        }

        [HttpPost("Excluir")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int livroId, int canalVendaId)
        {
            try
            {
                await _livroPrecoCanalVendaService.Remove(livroId, canalVendaId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir o preço do livro com LivroId {LivroId} e CanalVendaId {CanalVendaId}.", livroId, canalVendaId);
                TempData["ErrorMessage"] = "Erro ao excluir o preço do livro. Tente novamente mais tarde.";
                return RedirectToAction(nameof(Delete), new { livroId, canalVendaId });
            }
        }

        [HttpGet("Detalhes")]
        public async Task<IActionResult> Details(int? livroId, int? canalVendaId)
        {
            if (livroId == null || canalVendaId == null)
                return NotFound();

            try
            {
                var livroPrecoCanalVendaDto = await _livroPrecoCanalVendaService.GetById(livroId.Value, canalVendaId.Value);
                if (livroPrecoCanalVendaDto == null)
                    return NotFound();
                return View(livroPrecoCanalVendaDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter os detalhes do preço do livro com LivroId {LivroId} e CanalVendaId {CanalVendaId}.", livroId, canalVendaId);
                TempData["ErrorMessage"] = "Erro ao obter os detalhes do preço do livro. Tente novamente mais tarde.";
                return View("Error");
            }
        }
    }
}
