using TechsysLog.Application.DTOs;
using TechsysLog.Domain.Entities.Enum;

namespace TechsysLog.Application.Dtos.Pedidos
{
    public class PedidoDto
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string NumeroPedido { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public List<string> Itens { get; set; } = new();
        public decimal ValorTotal { get; set; }
        public EnderecoDto EnderecoEntrega { get; set; } = new();
        public Status Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataEnvio { get; set; }
        public bool Lida { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}