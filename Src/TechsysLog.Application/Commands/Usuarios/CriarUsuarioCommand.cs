using TechsysLog.Application.Commands;

namespace TechsysLog.Application.Commands.Usuarios
{
    /// <summary>
    /// Comando para criar um novo usuário.
    /// </summary>
    public class CriarUsuarioCommand : ICommand
    {
        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public string Nome { get; }

        /// <summary>
        /// E-mail do usuário.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Senha do usuário (texto puro, será transformada em hash).
        /// </summary>
        public string Senha { get; }

        /// <summary>
        /// Construtor do comando CriarUsuarioCommand.
        /// </summary>
        public CriarUsuarioCommand(string nome, string email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
        }
    }
}
