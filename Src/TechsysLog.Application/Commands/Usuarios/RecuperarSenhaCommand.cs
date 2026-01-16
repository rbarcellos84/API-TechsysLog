using TechsysLog.Application.Commands;

namespace TechsysLog.Application.Commands.Usuarios
{
    /// <summary>
    /// Comando para iniciar o processo de recuperação de senha de um usuário.
    /// Contém o e-mail que será utilizado para envio do token de recuperação.
    /// </summary>
    public class RecuperarSenhaCommand : ICommand
    {
        /// <summary>
        /// Endereço de e-mail do usuário que deseja recuperar a senha.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Construtor do comando de recuperação de senha.
        /// </summary>
        /// <param name="email">Endereço de e-mail do usuário.</param>
        public RecuperarSenhaCommand(string email)
        {
            Email = email;
        }
    }
}
