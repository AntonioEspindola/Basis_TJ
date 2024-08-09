using AspNetCore.Reporting;
using Livraria.Application.Interfaces;
using Livraria.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.WebUI.Controllers
{
    [Route("[controller]/[action]")]
    public class RelatoriosController : Controller
    {
        private readonly ILivroAutorAssuntoService _livroAutorAssuntoService;
        private readonly ILogger<RelatoriosController> _logger;

        public RelatoriosController(ILivroAutorAssuntoService livroAutorAssuntoService, ILogger<RelatoriosController> logger)
        {
            _livroAutorAssuntoService = livroAutorAssuntoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var relatorioData = await _livroAutorAssuntoService.GetRelatorioLivros();
                var reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "RelatorioLivroAutorAssunto.rdlc");

                if (!System.IO.File.Exists(reportPath))
                {
                    _logger.LogError("Arquivo de relatório não encontrado: {ReportPath}", reportPath);
                    TempData["ErrorMessage"] = "Arquivo de relatório não encontrado. Contate o suporte.";
                    return View("Error");
                }

                LocalReport localReport = new LocalReport(reportPath);
                localReport.AddDataSource("LivroAutorAssuntoDataSet", relatorioData);

                var result = localReport.Execute(RenderType.Pdf, 1, null, string.Empty);

                return File(result.MainStream, "application/pdf", "RelatorioLivroAutorAssunto.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar o relatório de livros, autores e assuntos.");
                TempData["ErrorMessage"] = "Erro ao gerar o relatório. Tente novamente mais tarde.";
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Ocorreu um erro inesperado ao tentar gerar o relatório. Por favor, tente novamente mais tarde."
                };
                return View("Error", errorModel);
            }
        }
    }
}


