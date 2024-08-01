using Livraria.API.Models.Erros;
using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssuntosController : ControllerBase
    {
        private readonly IAssuntoService _assuntoService;
        private readonly ILogger<AssuntosController> _logger;

        public AssuntosController(IAssuntoService assuntoService, ILogger<AssuntosController> logger)
        {
            _assuntoService = assuntoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssuntoDTO>>> Get()
        {
            try
            {
                var assuntos = await _assuntoService.GetAssuntos();
                if (assuntos == null || !assuntos.Any())
                {
                    return NotFound(new ErrorResponse { Message = "Nenhum assunto encontrado." });
                }
                return Ok(assuntos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter a lista de assuntos.");
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpGet("{id:int}", Name = "GetAssunto")]
        public async Task<ActionResult<AssuntoDTO>> Get(int id)
        {
            try
            {
                var assunto = await _assuntoService.GetById(id);
                if (assunto == null)
                {
                    return NotFound(new ErrorResponse { Message = $"Assunto com ID {id} não encontrado." });
                }
                return Ok(assunto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter o assunto com ID {Id}.", id);
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AssuntoDTO assuntoDto)
        {
            if (assuntoDto == null)
            {
                return BadRequest(new ErrorResponse { Message = "Dados do assunto inválidos." });
            }

            try
            {
                await _assuntoService.Add(assuntoDto);
                return CreatedAtRoute("GetAssunto", new { id = assuntoDto.Id }, assuntoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar um novo assunto.");
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] AssuntoDTO assuntoDto)
        {
            if (id != assuntoDto.Id)
            {
                return BadRequest(new ErrorResponse { Message = "ID do assunto não corresponde ao ID fornecido." });
            }

            if (assuntoDto == null)
            {
                return BadRequest(new ErrorResponse { Message = "Dados do assunto inválidos." });
            }

            try
            {
                await _assuntoService.Update(assuntoDto);
                return Ok(assuntoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar o assunto com ID {Id}.", id);
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<AssuntoDTO>> Delete(int id)
        {
            try
            {
                var assunto = await _assuntoService.GetById(id);
                if (assunto == null)
                {
                    return NotFound(new ErrorResponse { Message = $"Assunto com ID {id} não encontrado." });
                }

                await _assuntoService.Remove(id);
                return Ok(assunto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover o assunto com ID {Id}.", id);
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }
    }
}


