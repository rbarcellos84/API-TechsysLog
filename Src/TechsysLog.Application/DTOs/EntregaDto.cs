using TechsysLog.Domain.Entities;
using TechsysLog.Domain.Entities.Enum;

namespace TechsysLog.Application.Dtos.Entregas
{
    /// <summary>
    /// Data Transfer Object (DTO) para representar informações de uma entrega.
    /// Usado para transferência de dados entre a API e o cliente.
    /// </summary>
    public class EntregaDto
    {
        /// <summary>
        /// Identificador único da entrega.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Identificador do pedido associado à entrega.
        /// </summary>
        public Guid PedidoId { get; set; }

        /// <summary>
        /// Identificador do entregador responsável pela entrega.
        /// </summary>
        public Guid EntregadorId { get; set; }

        /// <summary>
        /// Data prevista para a entrega.
        /// </summary>
        public DateTime DataPrevista { get; set; }

        /// <summary>
        /// Data de conclusão da entrega (se já realizada).
        /// </summary>
        public DateTime? DataConclusao { get; set; }

        /// <summary>
        /// Endereço de destino da entrega.
        /// </summary>
        public Endereco EnderecoDestino { get; set; } = null!;

        /// <summary>
        /// Status atual da entrega (Ingressado, Processando, Enviado, Entregue, Cancelado).
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Observações adicionais sobre a entrega.
        /// </summary>
        public string? Observacoes { get; set; }

        /// <summary>
        /// Data de registro da entrega.
        /// </summary>
        public DateTime DataRegistro { get; set; }

        /// <summary>
        /// Data da última atualização da entrega.
        /// </summary>
        public DateTime? DataAtualizacao { get; set; }
    }
}
