namespace TechsysLog.Domain.Interfaces
{
    /// <summary>
    /// Contrato genérico para operações básicas de repositório.
    /// Apenas inserção, leitura e atualização.
    /// </summary>
    public interface IRepository<T>
    {
        /// <summary>
        /// Adiciona uma nova entidade.
        /// </summary>
        Task AddAsync(T entity, CancellationToken ct);

        /// <summary>
        /// Obtém uma entidade pelo identificador.
        /// </summary>
        Task<T?> ObterPorIdAsync(Guid id, CancellationToken ct);

        /// <summary>
        /// Lista todas as entidades cadastradas.
        /// </summary>
        Task<IEnumerable<T>> ListarTodosAsync(CancellationToken ct);

        /// <summary>
        /// Atualiza os dados de uma entidade existente.
        /// </summary>
        Task UpdateAsync(T entity, CancellationToken ct);
    }
}
