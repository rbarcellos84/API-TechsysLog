using TechsysLog.Domain.Entities;

namespace TechsysLog.Domain.Interfaces
{
    /// <summary>
    /// Repositório específico para Usuário.
    /// </summary>
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        /// <summary>
        /// Obtém um usuário pelo email.
        /// </summary>
        Task<Usuario?> ObterPorEmailAsync(string email, CancellationToken ct);

        /// <summary>
        /// Desativa um usuário (remoção lógica).
        /// </summary>
        Task DesativarAsync(Guid id, CancellationToken ct);
    }
}
