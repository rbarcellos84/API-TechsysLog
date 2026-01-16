using TechsysLog.Application.Commands.Notificacoes;
using TechsysLog.Domain.Entities;
using TechsysLog.Domain.Entities.Enum;
using TechsysLog.Domain.Interfaces;

namespace TechsysLog.Application.Handlers.Notificacoes
{
    /// <summary>
    /// Handler responsável por processar o comando de envio de notificação.
    /// </summary>
    public class EnviarNotificacaoHandler : ICommandHandler<EnviarNotificacaoCommand>
    {
        private readonly INotificacaoRepository _notificacaoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="EnviarNotificacaoHandler"/>.
        /// </summary>
        /// <param name="notificacaoRepository">Instância do repositório de notificações injetada via DI.</param>
        /// <param name="usuarioRepository">Instância do repositório de usuários injetada via DI.</param>
        public EnviarNotificacaoHandler(INotificacaoRepository notificacaoRepository, IUsuarioRepository usuarioRepository)
        {
            _notificacaoRepository = notificacaoRepository;
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Processa a lógica de negócio para a criação e persistência de uma nova notificação com tratamento de exceções.
        /// </summary>
        /// <param name="command">Objeto contendo os dados necessários para criar a notificação.</param>
        /// <param name="ct">Token de cancelamento para operações assíncronas.</param>
        /// <returns>Uma <see cref="Task"/> que representa a conclusão da operação assíncrona.</returns>
        public async Task HandleAsync(EnviarNotificacaoCommand command, CancellationToken ct)
        {
            try
            {
                var usuario = await _usuarioRepository.ObterPorIdAsync(command.UsuarioId, ct);

                if (usuario is null)
                    throw new InvalidOperationException("Usuário destinatário não encontrado.");

                var notificacao = new Notificacao(
                    id: Guid.NewGuid(),
                    usuarioId: command.UsuarioId,
                    numeroPedido: command.NumeroPedido,
                    status: command.Status
                );

                await _notificacaoRepository.AddAsync(notificacao, ct);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}