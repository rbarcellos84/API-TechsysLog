using DnsClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TechsysLog.Application.Commands.Usuarios;
using TechsysLog.Application.Handlers.Usuarios;
using TechsysLog.Domain.Exceptions;
using TechsysLog.Domain.Utils;
using TechsysLog.Web.Api.Security;

namespace TechsysLog.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly EfetuarLoginHandler _loginHandler;
        private readonly RecuperarSenhaHandler _recuperarSenhaHandler;
        private readonly CriarUsuarioHandler _criarUsuarioHandler;
        private readonly IConfiguration _configuration;

        public LoginController(
            EfetuarLoginHandler loginHandler,
            RecuperarSenhaHandler recuperarSenhaHandler,
            CriarUsuarioHandler criarUsuarioHandler,
            IConfiguration configuration)
        {
            _loginHandler = loginHandler;
            _recuperarSenhaHandler = recuperarSenhaHandler;
            _criarUsuarioHandler = criarUsuarioHandler;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] EfetuarLoginCommand command, CancellationToken ct)
        {
            var usuario = await _loginHandler.HandleAsync(command, ct);

            if (usuario == null)
                return Unauthorized(new { erro = "Credenciais inválidas." });

            if (!command.Email.Equals(usuario.Email) || !SenhaHelper.ValidarSenha(command.Senha, usuario.SenhaHash))
                return BadRequest(new { erro = "E-mail ou senha incorretos." });

            var token = JwtSecurity.GenerateToken(usuario.Email, _configuration);

            return Ok(new
            {
                mensagem = "Autenticação realizada com sucesso.",
                token = token
            });
        }
    }
}