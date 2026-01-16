using TechsysLog.Domain.Events.Interfaces;

namespace TechsysLog.Domain.Events
{
    public sealed class PedidoEntregueEvent : IDomainEvent
    {
        public Guid PedidoId { get; }
        public string NumeroPedido { get; }
        public DateTime DataHoraEntrega { get; }
        public string? Observacoes { get; }
        public DateTime OcorreuEm { get; } = DateTime.UtcNow;

        public PedidoEntregueEvent(Guid pedidoId, string numeroPedido, DateTime dataHoraEntrega, string? observacoes = null)
        {
            PedidoId = pedidoId;
            NumeroPedido = numeroPedido;
            DataHoraEntrega = dataHoraEntrega;
            Observacoes = observacoes;
        }
    }
}
