using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechsysLog.Application.Commands.Usuarios;
using TechsysLog.Application.Handlers.Usuarios;
using TechsysLog.Web.Api.Security;
using TechsysLog.Domain.Exceptions;

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

        public LoginController(
            EfetuarLoginHandler loginHandler,
            RecuperarSenhaHandler recuperarSenhaHandler,
            CriarUsuarioHandler criarUsuarioHandler)
        {
            _loginHandler = loginHandler;
            _recuperarSenhaHandler = recuperarSenhaHandler;
            _criarUsuarioHandler = criarUsuarioHandler;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] EfetuarLoginCommand command, CancellationToken ct)
        {
            try
            {
                var usuario = await _loginHandler.HandleAsync(command, ct);

                if (usuario == null)
                    return Unauthorized(new { erro = "Credenciais inválidas." });

                // Validação de segurança básica
                if (!command.Senha.Equals(usuario.SenhaHash) || !command.Email.Equals(usuario.Email))
                    return BadRequest(new { erro = "E-mail ou senha incorretos." });

                // Geração do Token que resolve o erro 401 dos outros endpoints
                var token = JwtSecurity.GenerateToken(usuario.Email);

                return Ok(new
                {
                    mensagem = "Autenticação realizada com sucesso.",
                    token = token
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao processar o login.", detalhes = ex.Message });
            }
        }

        [HttpPost("recuperar-senha")]
        public async Task<IActionResult> RecuperarSenha([FromBody] RecuperarSenhaCommand command, CancellationToken ct)
        {
            try
            {
                await _recuperarSenhaHandler.HandleAsync(command, ct);
                return Ok(new { mensagem = "A nova senha foi enviada para o e-mail cadastrado." });
            }
            catch (DomainException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao processar recuperação de senha." });
            }
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] CriarUsuarioCommand command, CancellationToken ct)
        {
            try
            {
                await _criarUsuarioHandler.HandleAsync(command, ct);
                return Ok(new { mensagem = "Usuário cadastrado com sucesso." });
            }
            catch (DomainException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao realizar cadastro de usuário." });
            }
        }
    }
}