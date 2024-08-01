using Livraria.API.Models.Erros;
using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivrosController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivroDTO>>> Get()
        {
            try
            {
                var livros = await _livroService.GetLivros();
                if (livros == null || !livros.Any())
                {
                    return NotFound(new ErrorResponse { Message = "Nenhum livro encontrado." });
                }
                return Ok(livros);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Erro ao obter a lista de livros.");
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpGet("{id:int}", Name = "GetLivro")]
        public async Task<ActionResult<LivroDTO>> Get(int id)
        {
            try
            {
                var livro = await _livroService.GetById(id);
                if (livro == null)
                {
                    return NotFound(new ErrorResponse { Message = $"Livro com ID {id} não encontrado." });
                }
                return Ok(livro);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Erro ao obter o livro com ID {Id}.", id);
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LivroDTO livroDto)
        {
            if (livroDto == null)
            {
                return BadRequest(new ErrorResponse { Message = "Dados do livro inválidos." });
            }

            try
            {
               await _livroService.Add(livroDto);
                return CreatedAtRoute("GetLivro", new { id = livroDto.Id }, livroDto);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Erro ao criar um novo livro.");
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] LivroDTO livroDto)
        {
            if (id != livroDto.Id)
            {
                return BadRequest(new ErrorResponse { Message = "ID do livro não corresponde ao ID fornecido." });
            }

            if (livroDto == null)
            {
                return BadRequest(new ErrorResponse { Message = "Dados do livro inválidos." });
            }

            try
            {
                await _livroService.Update(livroDto);
                return Ok(livroDto);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Erro ao atualizar o livro com ID {Id}.", id);
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<LivroDTO>> Delete(int id)
        {
            try
            {
                var livro = await _livroService.GetById(id);
                if (livro == null)
                {
                    return NotFound(new ErrorResponse { Message = $"Livro com ID {id} não encontrado." });
                }

                await _livroService.Remove(id);
                return Ok(livro);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Erro ao remover o livro com ID {Id}.", id);
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }
    }
}


