using MongoDB.Driver;
using TechsysLog.Domain.Entities;
using TechsysLog.Domain.Interfaces;
using TechsysLog.Infra.Data.Context;

namespace TechsysLog.Infra.Data.Repositories
{
    /// <summary>
    /// Repositório de notificações utilizando MongoDB.
    /// Responsável por operações de CRUD e consultas específicas.
    /// </summary>
    public class NotificacaoRepository : INotificacaoRepository
    {
        private readonly IMongoCollection<Notificacao> _notificacoes;

        /// <summary>
        /// Construtor que inicializa a coleção de notificações.
        /// </summary>
        /// <param name="context">Contexto do MongoDB contendo a coleção de notificações.</param>
        public NotificacaoRepository(MongoDbContext context)
        {
            _notificacoes = context.Notificacoes;
        }

        /// <summary>
        /// Adiciona uma nova notificação.
        /// </summary>
        /// <param name="notificacao">Objeto de notificação a ser inserido.</param>
        /// <param name="ct">Token de cancelamento da operação.</param>
        public async Task AddAsync(Notificacao notificacao, CancellationToken ct)
            => await _notificacoes.InsertOneAsync(notificacao, cancellationToken: ct);

        /// <summary>
        /// Obtém uma notificação pelo identificador único.
        /// </summary>
        /// <param name="id">Identificador da notificação.</param>
        /// <param name="ct">Token de cancelamento da operação.</param>
        /// <returns>Objeto de notificação ou null se não encontrado.</returns>
        public async Task<Notificacao?> ObterPorIdAsync(Guid id, CancellationToken ct)
            => await _notificacoes.Find(n => n.Id == id).FirstOrDefaultAsync(ct);

        /// <summary>
        /// Lista todas as notificações cadastradas no sistema.
        /// </summary>
        /// <param name="ct">Token de cancelamento da operação.</param>
        /// <returns>Lista de notificações.</returns>
        public async Task<IEnumerable<Notificacao>> ListarTodosAsync(CancellationToken ct)
            => await _notificacoes.Find(Builders<Notificacao>.Filter.Empty).ToListAsync(ct);

        /// <summary>
        /// Atualiza os dados de uma notificação existente.
        /// Exemplo: marcar como lida.
        /// </summary>
        /// <param name="notificacao">Objeto de notificação atualizado.</param>
        /// <param name="ct">Token de cancelamento da operação.</param>
        public async Task UpdateAsync(Notificacao notificacao, CancellationToken ct)
            => await _notificacoes.ReplaceOneAsync(n => n.Id == notificacao.Id, notificacao, cancellationToken: ct);

        /// <summary>
        /// Lista todas as notificações de um usuário específico.
        /// </summary>
        /// <param name="usuarioId">Identificador do usuário.</param>
        /// <param name="ct">Token de cancelamento da operação.</param>
        /// <returns>Lista de notificações do usuário informado.</returns>
        public async Task<IEnumerable<Notificacao>> ListarPorUsuarioAsync(Guid usuarioId, CancellationToken ct)
            => await _notificacoes.Find(n => n.UsuarioId == usuarioId).ToListAsync(ct);

        /// <summary>
        /// Lista todas as notificações não lidas de um usuário específico.
        /// </summary>
        /// <param name="usuarioId">Identificador do usuário.</param>
        /// <param name="ct">Token de cancelamento da operação.</param>
        /// <returns>Lista de notificações do usuário informado.</returns>
        public async Task<IEnumerable<Notificacao>> ListarNaoLidasPorUsuarioAsync(Guid usuarioId, CancellationToken ct)
            => await _notificacoes.Find(n => n.UsuarioId == usuarioId && n.Lida == false).ToListAsync(ct);

        /// <summary>
        /// Lista todas as notificações não lidas.
        /// </summary>
        /// <param name="ct">Token de cancelamento da operação.</param>
        /// <returns>Lista de notificações do usuário informado.</returns>
        public async Task<IEnumerable<Notificacao>> ListarNaoLidasAsync(CancellationToken ct)
            => await _notificacoes.Find(n => n.Lida == false).ToListAsync(ct);
    }
}
