using MongoDB.Driver;
using TechsysLog.Domain.Entities;
using TechsysLog.Domain.Entities.Enum;
using TechsysLog.Domain.Interfaces;
using TechsysLog.Infra.Data.Context;

namespace TechsysLog.Infra.Data.Repositories
{
    /// <summary>
    /// Repositório de pedidos utilizando MongoDB.
    /// Responsável por operações de CRUD.
    /// </summary>
    public class PedidoRepository : IPedidoRepository
    {
        private readonly IMongoCollection<Pedido> _pedidos;

        /// <summary>
        /// Construtor que inicializa a coleção de pedidos.
        /// </summary>
        /// <param name="context">Contexto do MongoDB contendo a coleção de pedidos.</param>
        public PedidoRepository(MongoDbContext context)
        {
            _pedidos = context.Pedidos;
        }

        /// <summary>
        /// Adiciona um novo pedido.
        /// </summary>
        /// <param name="pedido">Objeto pedido a ser inserido.</param>
        /// <param name="ct">Token de cancelamento da operação.</param>
        public async Task AddAsync(Pedido pedido, CancellationToken ct)
            => await _pedidos.InsertOneAsync(pedido, cancellationToken: ct);

        /// <summary>
        /// Obtém um pedido pelo identificador único.
        /// </summary>
        /// <param name="id">Identificador do pedido.</param>
        /// <param name="ct">Token de cancelamento da operação.</param>
        /// <returns>Objeto pedido ou null se não encontrado.</returns>
        public async Task<Pedido?> ObterPorIdAsync(Guid id, CancellationToken ct)
            => await _pedidos.Find(p => p.Id == id).FirstOrDefaultAsync(ct);

        /// <summary>
        /// Obtém um pedido pelo número único.
        /// </summary>
        /// <param name="numero">Número do pedido.</param>
        /// <param name="ct">Token de cancelamento da operação.</param>
        /// <returns>Objeto pedido ou null se não encontrado.</returns>
        public async Task<Pedido?> ObterPorNumeroAsync(string numero, CancellationToken ct)
            => await _pedidos.Find(p => p.NumeroPedido == numero).FirstOrDefaultAsync(ct);

        /// <summary>
        /// Lista todos os pedidos cadastrados.
        /// </summary>
        /// <param name="ct">Token de cancelamento da operação.</param>
        /// <returns>Lista de pedidos.</returns>
        public async Task<IEnumerable<Pedido>> ListarTodosAsync(CancellationToken ct)
            => await _pedidos.Find(Builders<Pedido>.Filter.Empty).ToListAsync(ct);

        /// <summary>
        /// Lista todos os pedidos não entregue.
        /// </summary>
        /// <param name="ct">Token de cancelamento da operação.</param>
        /// <returns>Lista de pedidos.</returns>
        public async Task<IEnumerable<Pedido>> ListarTodosNaoEntregueAsync(CancellationToken ct)
            => await _pedidos.Find(Builders<Pedido>.Filter.Ne(p => p.Status, Status.Entregue)).ToListAsync(ct);

        /// <summary>
        /// Lista todos os pedidos entregue.
        /// </summary>
        /// <param name="ct">Token de cancelamento da operação.</param>
        /// <returns>Lista de pedidos.</returns>
        public async Task<IEnumerable<Pedido>> ListarTodosEntregueAsync(CancellationToken ct)
            => await _pedidos.Find(Builders<Pedido>.Filter.Eq(p => p.Status, Status.Entregue)).ToListAsync(ct);

        /// <summary>
        /// Atualiza os dados de um pedido existente.
        /// </summary>
        /// <param name="pedido">Objeto pedido atualizado.</param>
        /// <param name="ct">Token de cancelamento da operação.</param>
        public async Task UpdateAsync(Pedido pedido, CancellationToken ct)
            => await _pedidos.ReplaceOneAsync(p => p.Id == pedido.Id, pedido, cancellationToken: ct);
    }
}
