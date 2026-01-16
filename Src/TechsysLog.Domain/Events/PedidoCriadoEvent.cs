using TechsysLog.Domain.Events.Interfaces;

namespace TechsysLog.Domain.Events
{
    public sealed class PedidoCriadoEvent : IDomainEvent
    {
        public Guid PedidoId { get; }
        public string NumeroPedido { get; }
        public DateTime CriadoEm { get; }
        public decimal ValorTotal { get; }
        public string Status { get; }
        public DateTime OcorreuEm { get; } = DateTime.UtcNow;

        public PedidoCriadoEvent(Guid pedidoId, string numeroPedido, DateTime criadoEm, decimal valorTotal, string status)
        {
            PedidoId = pedidoId;
            NumeroPedido = numeroPedido;
            CriadoEm = criadoEm;
            ValorTotal = valorTotal;
            Status = status;
        }
    }
}
