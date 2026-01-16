using System.Threading;
using System.Threading.Tasks;
using TechsysLog.Application.Queries.Interfaces;

namespace TechsysLog.Application.QueryHandlers.Interfaces
{
    /// <summary>
    /// Interface genérica para handlers de queries.
    /// Define o contrato para classes que processam uma query e retornam um resultado.
    /// </summary>
    /// <typeparam name="TQuery">
    /// Tipo da query que será processada. Deve implementar <see cref="IQuery{TResult}"/>.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// Tipo do resultado esperado após a execução da query.
    /// </typeparam>
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Executa a query fornecida e retorna o resultado correspondente.
        /// </summary>
        /// <param name="query">Instância da query que contém os parâmetros da consulta.</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
        /// <returns>Um objeto do tipo <typeparamref name="TResult"/> contendo o resultado da query.</returns>
        Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
    }
}
