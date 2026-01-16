using MongoDB.Driver;
using TechsysLog.Domain.Entities;
using TechsysLog.Domain.Interfaces;
using TechsysLog.Infra.Data.Context;

namespace TechsysLog.Infra.Data.Repositories
{
    /// <summary>
    /// Repositório de entregas utilizando MongoDB.
    /// Responsável por operações de CRUD e consultas específicas.
    /// </summary>
    public class EntregaRepository : IEntregaRepository
    {
        private readonly IMongoCollection<Entrega> _entregas;

        /// <summary>
        /// Construtor que inicializa a coleção de entregas.
        /// </summary>
        /// <param name="context">Contexto do MongoDB contendo a coleção de entregas.</param>
        public EntregaRepository(MongoDbContext context)
        {
            _entregas = context.Entregas;
        }

        /// <summary>
        /// Adiciona uma nova entrega.
        /// </summary>
        /// <param name="entrega">Objeto entrega a ser inserido.</param>
        /// <param name="ct">Token de cancelamento da operação.</param>
        public async Task AddAsync(Entrega entrega, CancellationToken ct)
            => await _entregas.InsertOneAsync(entrega, cancellationToken: ct);

        /// <summary>
        /// Obtém uma entrega pelo identificador único.
        /// </summary>
        /// <param name="id">Identificador da entrega.</param>
        /// <param name="ct">Token de cancelamento da operação.</param>
        /// <returns>Objeto entrega ou null se não encontrado.</returns>
        public async Task<Entrega?> ObterPorIdAsync(Guid id, CancellationToken ct)
            => await _entregas.Find(e => e.Id == id).FirstOrDefaultAsync(ct);

        /// <summary>
        /// Lista todas as entregas cadastradas.
        /// </summary>
        /// <param name="ct">Token de cancelamento da operação.</param>
        /// <returns>Lista de entregas.</returns>
        public async Task<IEnumerable<Entrega>> ListarTodosAsync(CancellationToken ct)
            => await _entregas.Find(Builders<Entrega>.Filter.Empty).ToListAsync(ct);

        /// <summary>
        /// Atualiza os dados de uma entrega existente.
        /// </summary>
        /// <param name="entrega">Objeto entrega atualizado.</param>
        /// <param name="ct">Token de cancelamento da operação.</param>
        public async Task UpdateAsync(Entrega entrega, CancellationToken ct)
            => await _entregas.ReplaceOneAsync(e => e.Id == entrega.Id, entrega, cancellationToken: ct);

        /// <summary>
        /// Lista todas as entregas associadas a um pedido específico.
        /// </summary>
        /// <param name="pedidoId">Identificador do pedido.</param>
        /// <param name="ct">Token de cancelamento da operação.</param>
        /// <returns>Lista de entregas vinculadas ao pedido informado.</returns>
        public async Task<IEnumerable<Entrega>> ListarPorPedidoAsync(Guid pedidoId, CancellationToken ct)
            => await _entregas.Find(e => e.PedidoId == pedidoId).ToListAsync(ct);
    }
}
