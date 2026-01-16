using TechsysLog.Application.Commands;

namespace TechsysLog.Application.Commands.Notificacoes
{
    /// <summary>
    /// Comando para marcar uma notificação como lida.
    /// </summary>
    public class MarcarNotificacaoComoLidaCommand : ICommand
    {
        /// <summary>
        /// Identificador único da notificação que será marcada como lida.
        /// </summary>
        public Guid NotificacaoId { get; }

        /// <summary>
        /// Identificador do usuário que está marcando a notificação como lida.
        /// </summary>
        public Guid UsuarioId { get; }

        /// <summary>
        /// Data e hora em que a notificação foi marcada como lida.
        /// </summary>
        public DateTime DataLeitura { get; }

        /// <summary>
        /// Construtor do comando de marcação de notificação como lida.
        /// </summary>
        /// <param name="notificacaoId">Identificador da notificação.</param>
        /// <param name="usuarioId">Identificador do usuário que marcou como lida.</param>
        /// <param name="dataLeitura">Data e hora da leitura.</param>
        public MarcarNotificacaoComoLidaCommand(Guid notificacaoId, Guid usuarioId, DateTime dataLeitura)
        {
            NotificacaoId = notificacaoId;
            UsuarioId = usuarioId;
            DataLeitura = dataLeitura;
        }
    }
}
