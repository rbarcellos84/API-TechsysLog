using TechsysLog.Domain.Entities;

namespace TechsysLog.Domain.Interfaces
{
    /// <summary>
    /// Repositório específico para Notificação.
    /// </summary>
    public interface INotificacaoRepository : IRepository<Notificacao>
    {
        /// <summary>
        /// Lista todas as notificações de um usuário específico.
        /// </summary>
        Task<IEnumerable<Notificacao>> ListarPorUsuarioAsync(Guid usuarioId, CancellationToken ct);

        /// <summary>
        /// Lista todas as notificações não lidas de um usuário específico.
        /// </summary>
        Task<IEnumerable<Notificacao>> ListarNaoLidasPorUsuarioAsync(Guid usuarioId, CancellationToken ct);

        /// <summary>
        /// Lista todas as notificações não lidas de um usuário específico.
        /// </summary>
        Task<IEnumerable<Notificacao>> ListarNaoLidasAsync(CancellationToken ct);
    }
}
