using TechsysLog.Application.Commands.Notificacoes;
using TechsysLog.Domain.Interfaces;

namespace TechsysLog.Application.Handlers.Notificacoes
{
    /// <summary>
    /// Handler responsável por processar o comando de marcação de notificação como lida.
    /// </summary>
    public class MarcarNotificacaoComoLidaHandler : ICommandHandler<MarcarNotificacaoComoLidaCommand>
    {
        private readonly INotificacaoRepository _notificacaoRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="MarcarNotificacaoComoLidaHandler"/>.
        /// </summary>
        /// <param name="notificacaoRepository">Instância do repositório de notificações injetada via DI.</param>
        public MarcarNotificacaoComoLidaHandler(INotificacaoRepository notificacaoRepository)
        {
            _notificacaoRepository = notificacaoRepository;
        }

        /// <summary>
        /// Processa a lógica de negócio para marcar uma notificação como lida com tratamento de exceções.
        /// </summary>
        /// <param name="command">Objeto contendo o identificador da notificação e do usuário.</param>
        /// <param name="ct">Token de cancelamento para operações assíncronas.</param>
        /// <returns>Uma <see cref="Task"/> que representa a conclusão da operação assíncrona.</returns>
        public async Task HandleAsync(MarcarNotificacaoComoLidaCommand command, CancellationToken ct)
        {
            try
            {
                var notificacao = await _notificacaoRepository.ObterPorIdAsync(command.NotificacaoId, ct);

                if (notificacao is null)
                    throw new InvalidOperationException("Notificação não encontrada.");

                if (notificacao.UsuarioId != command.UsuarioId)
                    throw new InvalidOperationException("Usuário não autorizado a marcar esta notificação como lida.");

                notificacao.MarcarComoLida();

                await _notificacaoRepository.UpdateAsync(notificacao, ct);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}