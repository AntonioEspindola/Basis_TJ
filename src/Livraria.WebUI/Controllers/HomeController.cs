using Microsoft.AspNetCore.Mvc;

namespace Livraria.WebUI.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
