using TechsysLog.Domain.Entities;

namespace TechsysLog.Domain.Interfaces
{
    /// <summary>
    /// Repositório específico para Entrega.
    /// </summary>
    public interface IEntregaRepository : IRepository<Entrega>
    {
        /// <summary>
        /// Lista todas as entregas associadas a um pedido específico.
        /// </summary>
        Task<IEnumerable<Entrega>> ListarPorPedidoAsync(Guid pedidoId, CancellationToken ct);
    }
}
