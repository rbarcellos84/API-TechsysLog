using System.Threading;
using System.Threading.Tasks;
using TechsysLog.Application.DTOs;
using TechsysLog.Application.Queries.Enums;
using TechsysLog.Application.QueryHandlers.Interfaces;
using TechsysLog.Domain.Entities.Enum;

namespace TechsysLog.Application.QueryHandlers.Enums
{
    /// <summary>
    /// Handler responsável por processar a query para obtenção da lista de status disponíveis no sistema.
    /// </summary>
    public class ObterListaStatusHandler : IQueryHandler<ObterListaStatusQuery, IEnumerable<EnumDto>>
    {
        /// <summary>
        /// Realiza a leitura do enum de Status e transforma em uma coleção de DTOs com código e descrição.
        /// </summary>
        /// <param name="query">Objeto de consulta para obtenção de status.</param>
        /// <param name="ct">Token de cancelamento para monitorar solicitações de interrupção da operação.</param>
        /// <returns>Uma coleção de <see cref="EnumDto"/> representando as opções de status.</returns>
        public Task<IEnumerable<EnumDto>> HandleAsync(ObterListaStatusQuery query, CancellationToken ct)
        {
            try
            {
                var lista = Enum.GetValues(typeof(Status))
                                .Cast<Status>()
                                .Select(s => new EnumDto
                                {
                                    Codigo = (int)s,
                                    Descricao = s.ToString()
                                });

                return Task.FromResult(lista);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}