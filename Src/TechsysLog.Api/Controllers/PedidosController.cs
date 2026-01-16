using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechsysLog.Application.Commands.Pedidos;
using TechsysLog.Application.Handlers.Pedidos;
using TechsysLog.Application.Queries.Enums;
using TechsysLog.Application.Queries.Pedidos;
using TechsysLog.Application.Queries.Usuarios;
using TechsysLog.Application.QueryHandlers.Enums;
using TechsysLog.Application.QueryHandlers.Pedidos;
using TechsysLog.Application.QueryHandlers.Usuarios;
using TechsysLog.Domain.Exceptions;

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
        private readonly ObterListaPedidosEntreguesHandler _entreguesHandler;
        private readonly ObterListaPedidosNaoEntreguesHandler _naoEntreguesHandler;
        private readonly ObterPedidoPorPedidoHandler _porIdHandler;
        private readonly ObterLoginPorEmailHandler _loginPorEmail;

        public PedidosController(
            CriarPedidoHandler criarPedidoHandler,
            AtualizarStatusPedidoHandler atualizarStatusPedidoHandler,
            CancelarPedidoHandler cancelarPedidoHandler,
            ObterListaStatusHandler statusHandler,
            ObterListaPedidosHandler todosHandler,
            ObterListaPedidosEntreguesHandler entreguesHandler,
            ObterListaPedidosNaoEntreguesHandler naoEntreguesHandler,
            ObterPedidoPorPedidoHandler porIdHandler,
            ObterLoginPorEmailHandler loginPorEmail)
        {
            _criarPedidoHandler = criarPedidoHandler;
            _atualizarStatusPedidoHandler = atualizarStatusPedidoHandler;
            _cancelarPedidoHandler = cancelarPedidoHandler;
            _statusHandler = statusHandler;
            _todosHandler = todosHandler;
            _entreguesHandler = entreguesHandler;
            _naoEntreguesHandler = naoEntreguesHandler;
            _porIdHandler = porIdHandler;
            _loginPorEmail = loginPorEmail;
        }

        [HttpGet("nao-entregues")]
        public async Task<IActionResult> ObterPedidosNaoEntregues(CancellationToken ct)
        {
            try
            {
                var pedidos = await _naoEntreguesHandler.HandleAsync(new ObterListaPedidosNaoEntreguesQuery(), ct);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao listar pedidos não entregues.", detalhes = ex.Message });
            }
        }

        [HttpGet("entregues")]
        public async Task<IActionResult> ObterPedidosEntregues(CancellationToken ct)
        {
            try
            {
                var pedidos = await _entreguesHandler.HandleAsync(new ObterListaPedidosEntreguesQuery(), ct);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao listar pedidos entregues.", detalhes = ex.Message });
            }
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

        [HttpGet("id-por-email/{email}")]
        public async Task<IActionResult> GetIdPorEmail(string email, CancellationToken ct)
        {
            try
            {
                var query = new ObterLoginPorEmailQuery(email);
                var usuario = await _loginPorEmail.HandleAsync(query, ct);
                if (usuario == null) return NotFound(new { mensagem = "Usuário não encontrado." });

                return Ok(new { usuarioId = usuario.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao processar busca de e-mail.", detalhes = ex.Message });
            }
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarPedido([FromBody] CriarPedidoCommand command, CancellationToken ct)
        {
            try
            {
                await _criarPedidoHandler.HandleAsync(command, ct);
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

                await _cancelarPedidoHandler.HandleAsync(command, ct);
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