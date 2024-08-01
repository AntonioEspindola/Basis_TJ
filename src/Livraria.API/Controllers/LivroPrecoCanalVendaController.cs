using Livraria.API.Models.Erros;
using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroPrecoCanalVendaController : ControllerBase
    {
        private readonly ILivroPrecoCanalVendaService _livroPrecoCanalVendaService;

        public LivroPrecoCanalVendaController(ILivroPrecoCanalVendaService livroPrecoCanalVendaService)
        {
            _livroPrecoCanalVendaService = livroPrecoCanalVendaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivroPrecoCanalVendaDTO>>> Get()
        {
            try
            {
                var livroPrecoCanalVenda = await _livroPrecoCanalVendaService.GetLivroPrecoCanalVenda();
                if (livroPrecoCanalVenda == null || !livroPrecoCanalVenda.Any())
                {
                    return NotFound(new ErrorResponse { Message = "Nenhum preço de livro por canal de venda encontrado." });
                }
                return Ok(livroPrecoCanalVenda);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Erro ao obter a lista de preços de livros por canal de venda.");
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpGet("{livroId:int}/{canalVendaId:int}", Name = "GetLivroPrecoCanalVenda")]
        public async Task<ActionResult<LivroPrecoCanalVendaDTO>> Get(int livroId, int canalVendaId)
        {
            try
            {
                var livroPrecoCanalVenda = await _livroPrecoCanalVendaService.GetById(livroId, canalVendaId);
                if (livroPrecoCanalVenda == null)
                {
                    return NotFound(new ErrorResponse { Message = $"Preço do livro com ID {livroId} para o canal de venda com ID {canalVendaId} não encontrado." });
                }
                return Ok(livroPrecoCanalVenda);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Erro ao obter o preço do livro com ID {LivroId} para o canal de venda com ID {CanalVendaId}.", livroId, canalVendaId);
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LivroPrecoCanalVendaDTO livroPrecoCanalVendaDto)
        {
            if (livroPrecoCanalVendaDto == null)
            {
                return BadRequest(new ErrorResponse { Message = "Dados do preço do livro por canal de venda inválidos." });
            }

            try
            {
                await _livroPrecoCanalVendaService.Add(livroPrecoCanalVendaDto);
                return CreatedAtRoute("GetLivroPrecoCanalVenda", new { livroId = livroPrecoCanalVendaDto.Livro_Codl, canalVendaId = livroPrecoCanalVendaDto.Livro_Codl }, livroPrecoCanalVendaDto);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Erro ao criar um novo preço de livro por canal de venda.");
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpPut("{livroId:int}/{canalVendaId:int}")]
        public async Task<ActionResult> Put(int livroId, int canalVendaId, [FromBody] LivroPrecoCanalVendaDTO livroPrecoCanalVendaDto)
        {
            if (livroId != livroPrecoCanalVendaDto.Livro_Codl || canalVendaId != livroPrecoCanalVendaDto.Livro_Codl)
            {
                return BadRequest(new ErrorResponse { Message = "ID do livro ou do canal de venda não correspondem aos IDs fornecidos." });
            }

            if (livroPrecoCanalVendaDto == null)
            {
                return BadRequest(new ErrorResponse { Message = "Dados do preço do livro por canal de venda inválidos." });
            }

            try
            {
                await _livroPrecoCanalVendaService.Update(livroPrecoCanalVendaDto);
                return Ok(livroPrecoCanalVendaDto);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Erro ao atualizar o preço do livro com ID {LivroId} para o canal de venda com ID {CanalVendaId}.", livroId, canalVendaId);
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpDelete("{livroId:int}/{canalVendaId:int}")]
        public async Task<ActionResult<LivroPrecoCanalVendaDTO>> Delete(int livroId, int canalVendaId)
        {
            try
            {
                var livroPrecoCanalVenda = await _livroPrecoCanalVendaService.GetById(livroId, canalVendaId);
                if (livroPrecoCanalVenda == null)
                {
                    return NotFound(new ErrorResponse { Message = $"Preço do livro com ID {livroId} para o canal de venda com ID {canalVendaId} não encontrado." });
                }

                await _livroPrecoCanalVendaService.Remove(livroId, canalVendaId);
                return Ok(livroPrecoCanalVenda);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Erro ao remover o preço do livro com ID {LivroId} para o canal de venda com ID {CanalVendaId}.", livroId, canalVendaId);
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }
    }
}
