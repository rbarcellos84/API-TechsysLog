using TechsysLog.Domain.Entities.Enum;

namespace TechsysLog.Application.Dtos.Notificacoes
{
    /// <summary>
    /// Data Transfer Object (DTO) para representar informações de uma notificação.
    /// Usado para transferência de dados entre a API e o cliente.
    /// </summary>
    public class NotificacaoDto
    {
        /// <summary>
        /// Identificador único da notificação.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Identificador do usuário destinatário da notificação.
        /// </summary>
        public Guid UsuarioId { get; set; }

        /// <summary>
        /// Número do pedido associado à notificação.
        /// </summary>
        public string NumeroPedido { get; set; } = string.Empty;

        /// <summary>
        /// Status atual do pedido no momento da notificação.
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Data e hora em que a notificação foi enviada.
        /// </summary>
        public DateTime? DataEnvio { get; set; }

        /// <summary>
        /// Indica se a notificação já foi lida pelo usuário.
        /// </summary>
        public bool Lida { get; set; }

        /// <summary>
        /// Data e hora em que a notificação foi marcada como lida (opcional).
        /// </summary>
        public DateTime? DataLeitura { get; set; }
    }
}
