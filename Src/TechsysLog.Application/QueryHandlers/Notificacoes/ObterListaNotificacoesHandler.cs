using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechsysLog.Application.Dtos.Notificacoes;
using TechsysLog.Application.Queries.Notificacoes;
using TechsysLog.Application.QueryHandlers.Interfaces;
using TechsysLog.Domain.Interfaces;

namespace TechsysLog.Application.QueryHandlers.Notificacoes
{
    /// <summary>
    /// Handler responsável por processar a query de listagem de todas as notificações.
    /// </summary>
    public class ObterListaNotificacoesHandler
        : IQueryHandler<ObterListaNotificacoesQuery, IEnumerable<NotificacaoDto>>
    {
        private readonly INotificacaoRepository _repository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ObterListaNotificacoesHandler"/>.
        /// </summary>
        /// <param name="repository">Instância do repositório de notificações injetada via DI.</param>
        public ObterListaNotificacoesHandler(INotificacaoRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Executa a consulta para recuperar todas as notificações registradas com tratamento de exceções.
        /// </summary>
        /// <param name="query">Objeto de consulta para listagem de notificações.</param>
        /// <param name="ct">Token de cancelamento para operações assíncronas.</param>
        /// <returns>Uma coleção de <see cref="NotificacaoDto"/> representando as notificações no sistema.</returns>
        public async Task<IEnumerable<NotificacaoDto>> HandleAsync(ObterListaNotificacoesQuery query, CancellationToken ct)
        {
            try
            {
                var notificacoes = await _repository.ListarTodosAsync(ct);

                return notificacoes.Select(n => new NotificacaoDto
                {
                    Id = n.Id,
                    UsuarioId = n.UsuarioId,
                    NumeroPedido = n.NumeroPedido,
                    Status = n.Status,
                    Lida = n.Lida,
                    DataEnvio = n.DataEnvio,
                    DataLeitura = n.DataLeitura
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}