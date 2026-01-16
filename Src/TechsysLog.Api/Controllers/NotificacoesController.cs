using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechsysLog.Application.Commands.Notificacoes;
using TechsysLog.Application.Handlers.Notificacoes;
using TechsysLog.Application.Queries.Notificacoes;
using TechsysLog.Application.QueryHandlers.Notificacoes;
using TechsysLog.Domain.Exceptions;

namespace Techylog.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacaoController : ControllerBase
    {
        private readonly MarcarNotificacaoComoLidaHandler _marcarNotificacaoComoLidaHandler;
        private readonly ObterListaNotificacoesHandler _obterTodasNotificacoesHandler;
        private readonly ObterNotificacoesNaoLidasHandler _obterNaoLidasHandler;
        private readonly ObterNotificacoesPorUsuarioHandler _obterPorUsuarioHandler;
        private readonly ObterNotificacoesPorUsuarioNaoLidasHandler _obterPorUsuarioNaoLidasHandler;

        public NotificacaoController(
            MarcarNotificacaoComoLidaHandler marcarNotificacaoComoLidaHandler,
            ObterListaNotificacoesHandler obterTodasNotificacoesHandler,
            ObterNotificacoesNaoLidasHandler obterNaoLidasHandler,
            ObterNotificacoesPorUsuarioHandler obterPorUsuarioHandler,
            ObterNotificacoesPorUsuarioNaoLidasHandler obterPorUsuarioNaoLidasHandler)
        {
            _marcarNotificacaoComoLidaHandler = marcarNotificacaoComoLidaHandler;
            _obterTodasNotificacoesHandler = obterTodasNotificacoesHandler;
            _obterNaoLidasHandler = obterNaoLidasHandler;
            _obterPorUsuarioHandler = obterPorUsuarioHandler;
            _obterPorUsuarioNaoLidasHandler = obterPorUsuarioNaoLidasHandler;
        }

        [HttpPost("marcar-lida")]
        public async Task<IActionResult> MarcarComoLida([FromBody] MarcarNotificacaoComoLidaCommand command, CancellationToken ct)
        {
            try
            {
                await _marcarNotificacaoComoLidaHandler.HandleAsync(command, ct);
                return Ok(new { mensagem = "Notificação marcada como lida com sucesso." });
            }
            catch (DomainException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao processar a leitura da notificação.", detalhes = ex.Message });
            }
        }

        [HttpGet("listar-todos")]
        public async Task<IActionResult> ObterTodas(CancellationToken ct)
        {
            try
            {
                var notificacoes = await _obterTodasNotificacoesHandler.HandleAsync(new ObterListaNotificacoesQuery(), ct);
                return Ok(notificacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao listar todas as notificações.", detalhes = ex.Message });
            }
        }

        [HttpGet("nao-lidas")]
        public async Task<IActionResult> ObterNaoLidas(CancellationToken ct)
        {
            try
            {
                var notificacoes = await _obterNaoLidasHandler.HandleAsync(new ObterNotificacoesNaoLidasQuery(), ct);
                return Ok(notificacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao buscar notificações não lidas.", detalhes = ex.Message });
            }
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> ObterPorUsuario(Guid usuarioId, CancellationToken ct)
        {
            try
            {
                var query = new ObterNotificacoesPorUsuarioQuery(usuarioId);
                var notificacoes = await _obterPorUsuarioHandler.HandleAsync(query, ct);
                return Ok(notificacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao buscar notificações do usuário.", detalhes = ex.Message });
            }
        }

        [HttpGet("usuario/{usuarioId}/nao-lidas")]
        public async Task<IActionResult> ObterPorUsuarioNaoLidas(Guid usuarioId, CancellationToken ct)
        {
            try
            {
                var query = new ObterNotificacoesPorUsuarioNaoLidasQuery(usuarioId);
                var notificacoes = await _obterPorUsuarioNaoLidasHandler.HandleAsync(query, ct);
                return Ok(notificacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao buscar notificações não lidas do usuário.", detalhes = ex.Message });
            }
        }
    }
}