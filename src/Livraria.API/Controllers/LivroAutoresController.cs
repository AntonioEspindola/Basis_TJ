using Livraria.API.Models.Erros;
using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroAutoresController : ControllerBase
    {
        private readonly ILivroAutorService _livroAutorService;
        private readonly ILogger<LivroAutoresController> _logger;

        public LivroAutoresController(ILivroAutorService livroAutorService, ILogger<LivroAutoresController> logger)
        {
            _livroAutorService = livroAutorService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivroAutorDTO>>> Get()
        {
            try
            {
                var livroAutores = await _livroAutorService.GetLivroAutores();
                if (livroAutores == null || !livroAutores.Any())
                {
                    return NotFound(new ErrorResponse { Message = "Nenhum livro-autor encontrado." });
                }
                return Ok(livroAutores);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter a lista de livro-autores.");
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpGet("{livroId:int}/{autorId:int}", Name = "GetLivroAutor")]
        public async Task<ActionResult<LivroAutorDTO>> Get(int livroId, int autorId)
        {
            try
            {
                var livroAutor = await _livroAutorService.GetById(livroId, autorId);
                if (livroAutor == null)
                {
                    return NotFound(new ErrorResponse { Message = $"LivroAutor com LivroId {livroId} e AutorId {autorId} não encontrado." });
                }
                return Ok(livroAutor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter o livro-autor com LivroId {LivroId} e AutorId {AutorId}.", livroId, autorId);
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LivroAutorDTO livroAutorDto)
        {
            if (livroAutorDto == null)
            {
                return BadRequest(new ErrorResponse { Message = "Dados do livro-autor inválidos." });
            }

            try
            {
                await _livroAutorService.Add(livroAutorDto);
                return CreatedAtRoute("GetLivroAutor", new { livroId = livroAutorDto.Livro_Codl, autorId = livroAutorDto.Autor_CodAu },
                    livroAutorDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar um novo livro-autor.");
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpPut("{livroId:int}/{autorId:int}")]
        public async Task<ActionResult> Put(int livroId, int autorId, [FromBody] LivroAutorDTO livroAutorDto)
        {
            if (livroId != livroAutorDto.Livro_Codl || autorId != livroAutorDto.Autor_CodAu)
            {
                return BadRequest(new ErrorResponse { Message = "ID do livro-autor não corresponde ao ID fornecido." });
            }

            if (livroAutorDto == null)
            {
                return BadRequest(new ErrorResponse { Message = "Dados do livro-autor inválidos." });
            }

            try
            {
                await _livroAutorService.Update(livroAutorDto);
                return Ok(livroAutorDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar o livro-autor com LivroId {LivroId} e AutorId {AutorId}.", livroId, autorId);
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpDelete("{livroId:int}/{autorId:int}")]
        public async Task<ActionResult<LivroAutorDTO>> Delete(int livroId, int autorId)
        {
            try
            {
                var livroAutor = await _livroAutorService.GetById(livroId, autorId);
                if (livroAutor == null)
                {
                    return NotFound(new ErrorResponse { Message = $"LivroAutor com LivroId {livroId} e AutorId {autorId} não encontrado." });
                }

                await _livroAutorService.Remove(livroId, autorId);
                return Ok(livroAutor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover o livro-autor com LivroId {LivroId} e AutorId {AutorId}.", livroId, autorId);
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }
    }
}


