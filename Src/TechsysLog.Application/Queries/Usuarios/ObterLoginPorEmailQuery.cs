using TechsysLog.Application.Dtos.Usuarios;
using TechsysLog.Application.Queries.Interfaces;

namespace TechsysLog.Application.Queries.Usuarios
{
    /// <summary>
    /// Query para obter os dados de login de um usuário específico
    /// a partir do seu endereço de e-mail.
    /// Retorna um objeto <see cref="UsuarioDto"/> correspondente.
    /// </summary>
    public class ObterLoginPorEmailQuery : IQuery<UsuarioDto>
    {
        /// <summary>
        /// Endereço de e-mail do usuário que será consultado.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Construtor da query para obter login por e-mail.
        /// </summary>
        /// <param name="email">Endereço de e-mail do usuário.</param>
        public ObterLoginPorEmailQuery(string email)
        {
            Email = email;
        }
    }
}
