using TechsysLog.Application.Commands;

namespace TechsysLog.Application.Handlers
{
    /// <summary>
    /// Interface genérica para handlers de comandos.
    /// </summary>
    /// <typeparam name="TCommand">Tipo do comando que será tratado.</typeparam>
    public interface ICommandHandlerRetorno<TCommand, TResult> where TCommand : ICommand
    {
        /// <summary>
        /// Executa o comando de forma assíncrona com retorno de resultado.
        /// </summary>
        Task<TResult> HandleAsync(TCommand command, CancellationToken ct);
    }
}
