using Livraria.API.Models.Erros;
using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly IAutorService _autorService;
        private readonly ILogger<AutoresController> _logger;

        public AutoresController(IAutorService autorService, ILogger<AutoresController> logger)
        {
            _autorService = autorService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorDTO>>> Get()
        {
            try
            {
                var autores = await _autorService.GetAutores();
                if (autores == null || !autores.Any())
                {
                    return NotFound(new ErrorResponse { Message = "Nenhum autor encontrado." });
                }
                return Ok(autores);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter a lista de autores.");
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpGet("{id:int}", Name = "GetAutor")]
        public async Task<ActionResult<AutorDTO>> Get(int id)
        {
            try
            {
                var autor = await _autorService.GetById(id);
                if (autor == null)
                {
                    return NotFound(new ErrorResponse { Message = $"Autor com ID {id} não encontrado." });
                }
                return Ok(autor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter o autor com ID {Id}.", id);
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AutorDTO autorDto)
        {
            if (autorDto == null)
            {
                return BadRequest(new ErrorResponse { Message = "Dados do autor inválidos." });
            }

            try
            {
               await _autorService.Add(autorDto);
                return CreatedAtRoute("GetAutor", new { id = autorDto.Id }, autorDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar um novo autor.");
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] AutorDTO autorDto)
        {
            if (id != autorDto.Id)
            {
                return BadRequest(new ErrorResponse { Message = "ID do autor não corresponde ao ID fornecido." });
            }

            if (autorDto == null)
            {
                return BadRequest(new ErrorResponse { Message = "Dados do autor inválidos." });
            }

            try
            {
                await _autorService.Update(autorDto);
                return Ok(autorDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar o autor com ID {Id}.", id);
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<AutorDTO>> Delete(int id)
        {
            try
            {
                var autor = await _autorService.GetById(id);
                if (autor == null)
                {
                    return NotFound(new ErrorResponse { Message = $"Autor com ID {id} não encontrado." });
                }

                await _autorService.Remove(id);
                return Ok(autor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover o autor com ID {Id}.", id);
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }
    }
}


