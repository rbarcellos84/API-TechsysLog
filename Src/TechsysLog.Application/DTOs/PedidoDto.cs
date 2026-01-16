using TechsysLog.Domain.Entities.Enum;

namespace TechsysLog.Application.Dtos.Pedidos
{
    /// <summary>
    /// Data Transfer Object (DTO) para representar informações de um pedido.
    /// Usado para transferência de dados entre a API e o cliente.
    /// </summary>
    public class PedidoDto
    {
        /// <summary>
        /// Identificador único do pedido.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Identificador do usuário associado ao pedido.
        /// </summary>
        public Guid UsuarioId { get; set; }

        /// <summary>
        /// Número único do pedido.
        /// </summary>
        public string NumeroPedido { get; set; } = string.Empty;

        /// <summary>
        /// Descrição geral do pedido.
        /// </summary>
        public string Descricao { get; set; } = string.Empty;

        /// <summary>
        /// Lista de itens do pedido.
        /// </summary>
        public List<string> Itens { get; set; } = new();

        /// <summary>
        /// Valor total do pedido.
        /// </summary>
        public decimal ValorTotal { get; set; }

        /// <summary>
        /// Status atual do pedido (Ingressado, Processando, Enviado, Entregue, Cancelado).
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Data de criação do pedido.
        /// </summary>
        public DateTime DataCriacao { get; set; }

        /// <summary>
        /// Data da última atualização do pedido (se houver).
        /// </summary>
        public DateTime? DataAtualizacao { get; set; }
    }
}
