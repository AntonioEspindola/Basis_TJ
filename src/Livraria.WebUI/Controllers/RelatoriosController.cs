using Livraria.Application.Interfaces;
using Livraria.WebUI.Helpers;
using Livraria.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;

namespace Livraria.WebUI.Controllers
{
    [Route("[controller]/[action]")]
    public class RelatoriosController : Controller
    {
        private readonly ILivroAutorAssuntoService _livroAutorAssuntoService;
        private readonly ILogger<RelatoriosController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RelatoriosController(ILivroAutorAssuntoService livroAutorAssuntoService, ILogger<RelatoriosController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _livroAutorAssuntoService = livroAutorAssuntoService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var relatorioData = await _livroAutorAssuntoService.GetRelatorioLivros();
                var dataTableLivros = DataTableExtensions.ConvertListToDataTable(relatorioData);

                string wwwRooFolder = _webHostEnvironment.WebRootPath;
                var reportPath = Path.Combine(wwwRooFolder, "Reports", "RelLivroAutorAssunto.rdlc");
                //var reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "RelLivroAutorAssunto.rdlc");

                if (!System.IO.File.Exists(reportPath))
                {
                    _logger.LogError("Arquivo de relatório não encontrado: {ReportPath}", reportPath);
                    TempData["ErrorMessage"] = "Arquivo de relatório não encontrado. Contate o suporte.";
                    return View("Error");
                }

                var localReport = new LocalReport
                {
                    ReportPath = reportPath
                };

                localReport.DataSources.Add(new ReportDataSource("LivroAutorAssuntoDataSet", dataTableLivros));

                var result = localReport.Render("PDF");

                return File(result, "application/pdf", "RelatorioLivroAutorAssunto.pdf");
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


