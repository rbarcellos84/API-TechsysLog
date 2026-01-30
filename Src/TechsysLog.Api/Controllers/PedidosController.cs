using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TechsysLog.Application.Commands.Pedidos;
using TechsysLog.Application.Handlers.Pedidos;
using TechsysLog.Application.Queries.Enums;
using TechsysLog.Application.Queries.Pedidos;
using TechsysLog.Application.QueryHandlers.Enums;
using TechsysLog.Application.QueryHandlers.Pedidos;
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
            var pedidos = await _todosHandler.HandleAsync(new ObterListaPedidosQuery(), ct);
            return Ok(pedidos);
        }

        [HttpGet("obter-por-numero/{numeroPedido}")]
        public async Task<IActionResult> ObterPorNumero(string numeroPedido, CancellationToken ct)
        {
            var pedido = await _porIdHandler.HandleAsync(new ObterPedidoPorNumeroQuery(numeroPedido), ct);
            if (pedido == null) return NotFound(new { mensagem = "Pedido não encontrado." });
            return Ok(pedido);
        }

        [HttpPut("marcar-como-lido/{numeroPedido}")]
        public async Task<IActionResult> MarcarComoLito(string numeroPedido, CancellationToken ct)
        {
            await _marcarPedidoHandler.HandleAsync(new MarcarPedidoCommand(numeroPedido), ct);
            return Ok(new { mensagem = "Pedido marcado como lido com sucesso." });
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarPedido([FromBody] CriarPedidoCommand command, CancellationToken ct)
        {
            await _criarPedidoHandler.HandleAsync(command, ct);
            await _hubContext.Clients.All.SendAsync("AtualizarGraficos");
            return Ok(new { mensagem = "Pedido registrado com sucesso." });
        }

        [HttpPut("atualizar-status")]
        public async Task<IActionResult> AtualizarStatus([FromBody] AtualizarStatusPedidoCommand command, CancellationToken ct)
        {
            await _atualizarStatusPedidoHandler.HandleAsync(command, ct);
            await _hubContext.Clients.All.SendAsync("AtualizarGraficos");
            return Ok(new { mensagem = "Status do pedido atualizado com sucesso!" });
        }

        [HttpPut("{id}/cancelar")]
        public async Task<IActionResult> CancelarPedido(Guid id, [FromBody] CancelarPedidoCommand command, CancellationToken ct)
        {
            if (id != command.Id)
                return BadRequest(new { erro = "Id do pedido não corresponde ao informado." });

            var pedido = await _porIdPedidoHandler.HandleAsync(new ObterPedidoPorIdQuery(command.Id), ct);
            if (pedido == null) return NotFound(new { mensagem = "Pedido não encontrado." });

            await _cancelarPedidoHandler.HandleAsync(command, ct);
            await _hubContext.Clients.All.SendAsync("AtualizarGraficos");
            return Ok(new { mensagem = "Pedido cancelado com sucesso." });
        }

        [HttpGet("status")]
        public async Task<IActionResult> ObterListaStatus(CancellationToken ct)
        {
            var lista = await _statusHandler.HandleAsync(new ObterListaStatusQuery(), ct);
            return Ok(lista);
        }
    }
}