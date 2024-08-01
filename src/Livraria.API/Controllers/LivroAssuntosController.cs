using Livraria.API.Models.Erros;
using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroAssuntosController : ControllerBase
    {
        private readonly ILivroAssuntoService _livroAssuntoService;
        private readonly ILogger<LivroAssuntosController> _logger;

        public LivroAssuntosController(ILivroAssuntoService livroAssuntoService, ILogger<LivroAssuntosController> logger)
        {
            _livroAssuntoService = livroAssuntoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivroAssuntoDTO>>> Get()
        {
            try
            {
                var livroAssuntos = await _livroAssuntoService.GetLivroAssuntos();
                if (livroAssuntos == null || !livroAssuntos.Any())
                {
                    return NotFound(new ErrorResponse { Message = "Nenhum livro-assunto encontrado." });
                }
                return Ok(livroAssuntos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter a lista de livro-assuntos.");
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpGet("{livroId:int}/{assuntoId:int}", Name = "GetLivroAssunto")]
        public async Task<ActionResult<LivroAssuntoDTO>> Get(int livroId, int assuntoId)
        {
            try
            {
                var livroAssunto = await _livroAssuntoService.GetById(livroId, assuntoId);
                if (livroAssunto == null)
                {
                    return NotFound(new ErrorResponse { Message = $"LivroAssunto com LivroId {livroId} e AssuntoId {assuntoId} não encontrado." });
                }
                return Ok(livroAssunto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter o livro-assunto com LivroId {LivroId} e AssuntoId {AssuntoId}.", livroId, assuntoId);
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LivroAssuntoDTO livroAssuntoDto)
        {
            if (livroAssuntoDto == null)
            {
                return BadRequest(new ErrorResponse { Message = "Dados do livro-assunto inválidos." });
            }

            try
            {
                await _livroAssuntoService.Add(livroAssuntoDto);
                return CreatedAtRoute("GetLivroAssunto", new { livroId = livroAssuntoDto.Livro_Codl, assuntoId = livroAssuntoDto.Assunto_CodAs },
                    livroAssuntoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar um novo livro-assunto.");
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpPut("{livroId:int}/{assuntoId:int}")]
        public async Task<ActionResult> Put(int livroId, int assuntoId, [FromBody] LivroAssuntoDTO livroAssuntoDto)
        {
            if (livroId != livroAssuntoDto.Livro_Codl || assuntoId != livroAssuntoDto.Assunto_CodAs)
            {
                return BadRequest(new ErrorResponse { Message = "ID do livro-assunto não corresponde ao ID fornecido." });
            }

            if (livroAssuntoDto == null)
            {
                return BadRequest(new ErrorResponse { Message = "Dados do livro-assunto inválidos." });
            }

            try
            {
                await _livroAssuntoService.Update(livroAssuntoDto);
                return Ok(livroAssuntoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar o livro-assunto com LivroId {LivroId} e AssuntoId {AssuntoId}.", livroId, assuntoId);
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpDelete("{livroId:int}/{assuntoId:int}")]
        public async Task<ActionResult<LivroAssuntoDTO>> Delete(int livroId, int assuntoId)
        {
            try
            {
                var livroAssunto = await _livroAssuntoService.GetById(livroId, assuntoId);
                if (livroAssunto == null)
                {
                    return NotFound(new ErrorResponse { Message = $"LivroAssunto com LivroId {livroId} e AssuntoId {assuntoId} não encontrado." });
                }

                await _livroAssuntoService.Remove(livroId, assuntoId);
                return Ok(livroAssunto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover o livro-assunto com LivroId {LivroId} e AssuntoId {AssuntoId}.", livroId, assuntoId);
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }
    }
}


