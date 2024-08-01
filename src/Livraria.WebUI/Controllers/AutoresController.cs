using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.WebUI.Controllers
{
    [Route("[controller]/[action]")]
    public class AutoresController : Controller
    {
        private readonly IAutorService _autorService;

        public AutoresController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var autores = await _autorService.GetAutores();
            return View(autores);
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
                await _autorService.Add(autor);
                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }

        [HttpGet("Alterar")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var autorDto = await _autorService.GetById(id);
            if (autorDto == null) return NotFound();
            return View(autorDto);
        }

        [HttpPost("Alterar")]
        public async Task<IActionResult> Edit(AutorDTO autorDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _autorService.Update(autorDto);
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(autorDto);
        }

        [HttpGet("Excluir")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var autorDto = await _autorService.GetById(id);
            if (autorDto == null) return NotFound();
            return View(autorDto);
        }

        [HttpPost("Excluir")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _autorService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Detalhes")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var autorDto = await _autorService.GetById(id);
            if (autorDto == null) return NotFound();
            return View(autorDto);
        }
    }
}

