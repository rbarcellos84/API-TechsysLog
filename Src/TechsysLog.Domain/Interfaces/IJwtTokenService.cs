using TechsysLog.Domain.Entities;

namespace TechsysLog.Domain.Interfaces
{
    /// <summary>
    /// Define o contrato para serviços de geração de tokens JWT.
    /// Responsável por criar tokens de autenticação baseados em informações do usuário.
    /// </summary>
    public interface IJwtTokenService
    {
        /// <summary>
        /// Gera um token JWT para o usuário informado.
        /// </summary>
        /// <param name="usuario">Instância do usuário para o qual o token será gerado.</param>
        /// <returns>Uma string contendo o token JWT válido.</returns>
        string GerarToken(Usuario usuario);
    }
}
