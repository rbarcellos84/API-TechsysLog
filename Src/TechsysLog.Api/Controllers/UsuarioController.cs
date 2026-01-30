using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechsysLog.Application.Queries.Pedidos;
using TechsysLog.Application.Queries.Usuarios;
using TechsysLog.Application.QueryHandlers.Usuarios;

namespace TechsysLog.Web.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly ObterLoginPorEmailHandler _obterLoginPorEmailHandler;
        private readonly ObterLoginPorIdHandler _obterLoginPorIdHandler;

        public UsuarioController(
            ObterLoginPorEmailHandler obterLoginPorEmailHandler,
            ObterLoginPorIdHandler obterLoginPorIdHandler)
        {
            _obterLoginPorEmailHandler = obterLoginPorEmailHandler;
            _obterLoginPorIdHandler = obterLoginPorIdHandler;
        }

        [HttpGet("obter-id/{email}")]
        public async Task<IActionResult> GetIdPorEmail(string email, CancellationToken ct)
        {
            var usuario = await _obterLoginPorEmailHandler.HandleAsync(new ObterLoginPorEmailQuery(email), ct);
            if (usuario == null) return NotFound(new { mensagem = "Usuário não encontrado." });
            return Ok(new { usuarioId = usuario.Id });
        }

        [HttpGet("obter-email/{id}")]
        public async Task<IActionResult> GetEmailPorId(Guid id, CancellationToken ct)
        {
            var usuario = await _obterLoginPorIdHandler.HandleAsync(new ObterLoginPorIdQuery(id), ct);
            if (usuario == null) return NotFound(new { mensagem = "Usuário não encontrado." });
            return Ok(new { usuarioEmail = usuario.Email });
        }
    }
}
