using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TechsysLog.Application.Commands.Pedidos;
using TechsysLog.Application.Handlers.Pedidos;
using TechsysLog.Application.Queries.Enums;
using TechsysLog.Application.Queries.Pedidos;
using TechsysLog.Application.QueryHandlers.Enums;
using TechsysLog.Application.QueryHandlers.Pedidos;
using TechsysLog.Domain.Exceptions;
using TechsysLog.WebApi.Hubs;

namespace TechsysLog.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly CriarPedidoHandler _criarPedidoHandler;
        private readonly AtualizarStatusPedidoHandler _atualizarStatusPedidoHandler;
        private readonly CancelarPedidoHandler _cancelarPedidoHandler;
        private readonly ObterListaStatusHandler _statusHandler;
        private readonly ObterListaPedidosHandler _todosHandler;
        private readonly ObterPedidoPorNumeroHandler _porIdHandler;
        private readonly ObterPedidoPorIdHandler _porIdPedidoHandler;
        private readonly MarcarPedidoHandler _marcarPedidoHandler;
        private readonly IHubContext<PedidoHub> _hubContext;

        public PedidosController(
            CriarPedidoHandler criarPedidoHandler,
            AtualizarStatusPedidoHandler atualizarStatusPedidoHandler,
            CancelarPedidoHandler cancelarPedidoHandler,
            ObterListaStatusHandler statusHandler,
            ObterListaPedidosHandler todosHandler,
            ObterPedidoPorNumeroHandler porIdHandler,
            ObterPedidoPorIdHandler obterPedidoPorIdHandler,
            MarcarPedidoHandler marcarPedidoHandler,
            IHubContext<PedidoHub> hubContext)
        {
            _criarPedidoHandler = criarPedidoHandler;
            _atualizarStatusPedidoHandler = atualizarStatusPedidoHandler;
            _cancelarPedidoHandler = cancelarPedidoHandler;
            _statusHandler = statusHandler;
            _todosHandler = todosHandler;
            _porIdHandler = porIdHandler;
            _porIdPedidoHandler = obterPedidoPorIdHandler;
            _marcarPedidoHandler = marcarPedidoHandler;
            _hubContext = hubContext;
        }

        [HttpGet("obter-todos")]
        public async Task<IActionResult> ObterTodos(CancellationToken ct)
        {
            try
            {
                var pedidos = await _todosHandler.HandleAsync(new ObterListaPedidosQuery(), ct);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao obter lista de pedidos.", detalhes = ex.Message });
            }
        }

        [HttpGet("obter-por-numero/{numeroPedido}")]
        public async Task<IActionResult> ObterPorNumero(string numeroPedido, CancellationToken ct)
        {
            try
            {
                var pedido = await _porIdHandler.HandleAsync(new ObterPedidoPorNumeroQuery(numeroPedido), ct);
                if (pedido == null) return NotFound(new { mensagem = "Pedido não encontrado." });
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao buscar pedido por número.", detalhes = ex.Message });
            }
        }

        [HttpPut("marcar-como-lido/{numeroPedido}")]
        public async Task<IActionResult> MarcarComoLito(string numeroPedido, CancellationToken ct)
        {
            try
            {
                await _marcarPedidoHandler.HandleAsync(new MarcarPedidoCommand(numeroPedido), ct);

                return Ok(new { mensagem = "Pedido marcado como lido com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao marcar pedido como lido.", detalhes = ex.Message });
            }
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarPedido([FromBody] CriarPedidoCommand command, CancellationToken ct)
        {
            try
            {
                await _criarPedidoHandler.HandleAsync(command, ct);
                await _hubContext.Clients.All.SendAsync("AtualizarGraficos");

                return Ok(new { mensagem = "Pedido registrado com sucesso." });
            }
            catch (DomainException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro interno ao registrar pedido.", detalhes = ex.Message });
            }
        }

        [HttpPut("atualizar-status")]
        public async Task<IActionResult> AtualizarStatus([FromBody] AtualizarStatusPedidoCommand command, CancellationToken ct)
        {
            try
            {
                await _atualizarStatusPedidoHandler.HandleAsync(command, ct);
                await _hubContext.Clients.All.SendAsync("AtualizarGraficos");

                return Ok(new { mensagem = "Status do pedido atualizado com sucesso!" });
            }
            catch (DomainException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro interno ao atualizar status." });
            }
        }

        [HttpPut("{id}/cancelar")]
        public async Task<IActionResult> CancelarPedido(Guid id, [FromBody] CancelarPedidoCommand command, CancellationToken ct)
        {
            try
            {
                if (id != command.Id)
                    return BadRequest(new { erro = "Id do pedido não corresponde ao informado." });

                var pedido = await _porIdPedidoHandler.HandleAsync(new ObterPedidoPorIdQuery(command.Id), ct);
                if (pedido == null) return NotFound(new { mensagem = "Pedido não encontrado." });

                await _cancelarPedidoHandler.HandleAsync(command, ct);
                await _hubContext.Clients.All.SendAsync("AtualizarGraficos");

                return Ok(new { mensagem = "Pedido cancelado com sucesso." });
            }
            catch (DomainException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao tentar cancelar o pedido." });
            }
        }

        [HttpGet("status")]
        public async Task<IActionResult> ObterListaStatus(CancellationToken ct)
        {
            try
            {
                var lista = await _statusHandler.HandleAsync(new ObterListaStatusQuery(), ct);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao carregar lista de status." });
            }
        }
    }
}