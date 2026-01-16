using TechsysLog.Application.Commands;
using TechsysLog.Domain.Entities.Enum;

namespace TechsysLog.Application.Commands.Notificacoes
{
    /// <summary>
    /// Comando para enviar uma notificação vinculada a um pedido.
    /// </summary>
    public class EnviarNotificacaoCommand : ICommand
    {
        /// <summary>
        /// Identificador único do usuário destinatário da notificação.
        /// </summary>
        public Guid UsuarioId { get; }

        /// <summary>
        /// Número do pedido associado à notificação.
        /// </summary>
        public string NumeroPedido { get; }

        /// <summary>
        /// Status atual do pedido no momento da notificação.
        /// </summary>
        public Status Status { get; }

        /// <summary>
        /// Construtor do comando de envio de notificação.
        /// </summary>
        /// <param name="usuarioId">Identificador do usuário destinatário.</param>
        /// <param name="numeroPedido">Número do pedido associado.</param>
        /// <param name="status">Status atual do pedido.</param>
        public EnviarNotificacaoCommand(Guid usuarioId, string numeroPedido, Status status)
        {
            UsuarioId = usuarioId;
            NumeroPedido = numeroPedido;
            Status = status;
        }
    }
}
