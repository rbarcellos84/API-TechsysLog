using TechsysLog.Domain.Entities;

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
        /// Lista todas as entidades cadastradas nao entregue.
        /// </summary>
        Task<IEnumerable<Pedido>> ListarTodosNaoEntregueAsync(CancellationToken ct);

        /// <summary>
        /// Lista todas as entidades cadastradas entregue.
        /// </summary>
        Task<IEnumerable<Pedido>> ListarTodosEntregueAsync(CancellationToken ct);
    }
}
