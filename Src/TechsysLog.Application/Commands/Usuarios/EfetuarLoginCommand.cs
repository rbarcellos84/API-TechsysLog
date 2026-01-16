using TechsysLog.Application.Commands;

namespace TechsysLog.Application.Commands.Usuarios
{
    /// <summary>
    /// Comando para efetuar o login de um usuário.
    /// Contém as credenciais necessárias para autenticação.
    /// </summary>
    public class EfetuarLoginCommand : ICommand
    {
        /// <summary>
        /// Endereço de e-mail do usuário que deseja efetuar login.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Senha do usuário em texto puro (será validada contra o hash armazenado).
        /// </summary>
        public string Senha { get; }

        /// <summary>
        /// Construtor do comando de login.
        /// </summary>
        /// <param name="email">Endereço de e-mail do usuário.</param>
        /// <param name="senha">Senha do usuário.</param>
        public EfetuarLoginCommand(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }
    }
}
