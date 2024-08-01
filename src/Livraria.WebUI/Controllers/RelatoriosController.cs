using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AspNetCore.Reporting;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.WebUI.Controllers
{
    [Route("[controller]/[action]")]
    public class RelatoriosController : Controller
    {
        private readonly ILivroAutorAssuntoService _livroAutorAssuntoService;

        public RelatoriosController(ILivroAutorAssuntoService livroAutorAssuntoService)
        {
            _livroAutorAssuntoService = livroAutorAssuntoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var relatorioData = await _livroAutorAssuntoService.GetRelatorioLivros();
            var reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "RelatorioLivroAutorAssunto.rdlc");

            if (!System.IO.File.Exists(reportPath))
            {
                //return View("Error", new ErrorViewModel { RequestId = "Report file not found." });
            }

            LocalReport localReport = new LocalReport(reportPath);
            localReport.AddDataSource("LivroAutorAssuntoDataSet", relatorioData);

            var result = localReport.Execute(RenderType.Pdf, 1, null, string.Empty);

            return File(result.MainStream, "application/pdf", "RelatorioLivroAutorAssunto.pdf");
        }
    }
}

