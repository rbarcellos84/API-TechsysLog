using TechsysLog.Domain.Entities;
using TechsysLog.Domain.Entities.Enum;

namespace TechsysLog.Domain.Interfaces
{
    /// <summary>
    /// Repositório específico para Pedido.
    /// </summary>
    public interface IPedidoRepository : IRepository<Pedido>
    {
        /// <summary>
        /// Obtém um pedido pelo número único.
        /// </summary>
        Task<Pedido?> ObterPorNumeroAsync(string numero, CancellationToken ct);

        /// <summary>
        /// Lista todas as entidades cadastradas.
        /// </summary>
        Task<IEnumerable<Pedido>> ListarTodosPorEstadoAsync(Status status, CancellationToken ct);
    }
}
