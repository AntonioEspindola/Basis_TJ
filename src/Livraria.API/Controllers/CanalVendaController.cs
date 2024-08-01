using Livraria.API.Models.Erros;
using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanalVendaController : ControllerBase
    {
        private readonly ICanalVendaService _canalVendaService;

        public CanalVendaController(ICanalVendaService canalVendaService)
        {
            _canalVendaService = canalVendaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CanalVendaDTO>>> Get()
        {
            try
            {
                var CanalVenda = await _canalVendaService.GetCanalVendas();
                if (CanalVenda == null || !CanalVenda.Any())
                {
                    return NotFound(new ErrorResponse { Message = "Nenhum canal de venda encontrado." });
                }
                return Ok(CanalVenda);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Erro ao obter a lista de canais de venda.");
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpGet("{id:int}", Name = "GetCanalVenda")]
        public async Task<ActionResult<CanalVendaDTO>> Get(int id)
        {
            try
            {
                var canalVenda = await _canalVendaService.GetById(id);
                if (canalVenda == null)
                {
                    return NotFound(new ErrorResponse { Message = $"Canal de venda com ID {id} não encontrado." });
                }
                return Ok(canalVenda);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Erro ao obter o canal de venda com ID {Id}.", id);
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CanalVendaDTO canalVendaDto)
        {
            if (canalVendaDto == null)
            {
                return BadRequest(new ErrorResponse { Message = "Dados do canal de venda inválidos." });
            }

            try
            {
                await _canalVendaService.Add(canalVendaDto);
                return CreatedAtRoute("GetCanalVenda", new { id = canalVendaDto.Id }, canalVendaDto);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Erro ao criar um novo canal de venda.");
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CanalVendaDTO canalVendaDto)
        {
            if (id != canalVendaDto.Id)
            {
                return BadRequest(new ErrorResponse { Message = "ID do canal de venda não corresponde ao ID fornecido." });
            }

            if (canalVendaDto == null)
            {
                return BadRequest(new ErrorResponse { Message = "Dados do canal de venda inválidos." });
            }

            try
            {
                await _canalVendaService.Update(canalVendaDto);
                return Ok(canalVendaDto);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Erro ao atualizar o canal de venda com ID {Id}.", id);
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CanalVendaDTO>> Delete(int id)
        {
            try
            {
                var canalVenda = await _canalVendaService.GetById(id);
                if (canalVenda == null)
                {
                    return NotFound(new ErrorResponse { Message = $"Canal de venda com ID {id} não encontrado." });
                }

                await _canalVendaService.Remove(id);
                return Ok(canalVenda);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Erro ao remover o canal de venda com ID {Id}.", id);
                return StatusCode(500, new ErrorResponse { Message = "Erro interno do servidor. Tente novamente mais tarde." });
            }
        }
    }
}
