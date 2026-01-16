using TechsysLog.Application.Commands.Usuarios;
using TechsysLog.Domain.Entities;
using TechsysLog.Domain.Interfaces;

namespace TechsysLog.Application.Handlers.Usuarios
{
    /// <summary>
    /// Handler responsável por buscar os dados do usuário para o processo de autenticação.
    /// </summary>
    public class EfetuarLoginHandler : ICommandHandlerRetorno<EfetuarLoginCommand, Usuario?>
    {
        private readonly IUsuarioRepository _repository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="EfetuarLoginHandler"/>.
        /// </summary>
        /// <param name="repository">Instância do repositório de usuários injetada via DI.</param>
        public EfetuarLoginHandler(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Recupera um usuário através do e-mail informado para validação de credenciais.
        /// </summary>
        /// <param name="command">Objeto contendo o e-mail do usuário para tentativa de login.</param>
        /// <param name="ct">Token de cancelamento para operações assíncronas.</param>
        /// <returns>Uma <see cref="Task"/> contendo a entidade <see cref="Usuario"/> se localizado, ou null caso contrário.</returns>
        public async Task<Usuario?> HandleAsync(EfetuarLoginCommand command, CancellationToken ct)
        {
            var usuario = await _repository.ObterPorEmailAsync(command.Email, ct);
            return usuario;
        }
    }
}