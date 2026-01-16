using System.Threading;
using System.Threading.Tasks;
using TechsysLog.Application.Commands;

namespace TechsysLog.Application.Handlers
{
    /// <summary>
    /// Interface genérica para handlers de comandos.
    /// </summary>
    /// <typeparam name="TCommand">Tipo do comando que será tratado.</typeparam>
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        /// <summary>
        /// Executa o comando de forma assíncrona.
        /// </summary>
        Task HandleAsync(TCommand command, CancellationToken ct);
    }
}
