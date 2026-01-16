namespace TechsysLog.Domain.Events.Interfaces
{
    /// <summary>
    /// Interface base para todos os eventos de domínio.
    /// Deve ser implementada por qualquer evento que represente
    /// uma mudança significativa no estado do sistema.
    /// </summary>
    public interface IDomainEvent
    {
        /// <summary>
        /// Data e hora em que o evento ocorreu.
        /// </summary>
        DateTime OcorreuEm { get; }
    }
}
