using Microsoft.AspNetCore.Mvc;

namespace Livraria.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        // Exibe a página de erro
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Define a mensagem de erro e redireciona para a página de erro
        public IActionResult ShowError(string message)
        {
            TempData["ErrorMessage"] = message;
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Error(string message)
        {
            return View();
        }
    }
}

