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
    /// Handler responsável por processar a query de recuperação de notificações que ainda não foram lidas.
    /// </summary>
    public class ObterNotificacoesNaoLidasHandler
        : IQueryHandler<ObterNotificacoesNaoLidasQuery, IEnumerable<NotificacaoDto>>
    {
        private readonly INotificacaoRepository _repository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ObterNotificacoesNaoLidasHandler"/>.
        /// </summary>
        /// <param name="repository">Instância do repositório de notificações injetada via DI.</param>
        public ObterNotificacoesNaoLidasHandler(INotificacaoRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Executa a consulta filtrando apenas notificações com o estado de leitura falso, com tratamento de exceções.
        /// </summary>
        /// <param name="query">Objeto de consulta para notificações não lidas.</param>
        /// <param name="ct">Token de cancelamento para operações assíncronas.</param>
        /// <returns>Uma coleção de <see cref="NotificacaoDto"/> contendo os dados das notificações pendentes.</returns>
        public async Task<IEnumerable<NotificacaoDto>> HandleAsync(ObterNotificacoesNaoLidasQuery query, CancellationToken ct)
        {
            try
            {
                var notificacoes = await _repository.ListarNaoLidasAsync(ct);

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