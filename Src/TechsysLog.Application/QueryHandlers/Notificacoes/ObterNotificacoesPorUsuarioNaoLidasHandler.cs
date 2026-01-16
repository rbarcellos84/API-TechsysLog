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
    /// Handler responsável por processar a query de recuperação de notificações não lidas filtradas por um usuário específico.
    /// </summary>
    public class ObterNotificacoesPorUsuarioNaoLidasHandler : IQueryHandler<ObterNotificacoesPorUsuarioNaoLidasQuery, IEnumerable<NotificacaoDto>>
    {
        private readonly INotificacaoRepository _repository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ObterNotificacoesPorUsuarioNaoLidasHandler"/>.
        /// </summary>
        /// <param name="repository">Instância do repositório de notificações injetada via DI.</param>
        public ObterNotificacoesPorUsuarioNaoLidasHandler(INotificacaoRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Executa a consulta para listar notificações pendentes de leitura para um usuário específico com tratamento de exceções.
        /// </summary>
        /// <param name="query">Objeto de consulta contendo o identificador do usuário.</param>
        /// <param name="ct">Token de cancelamento para operações assíncronas.</param>
        /// <returns>Uma coleção de <see cref="NotificacaoDto"/> mapeada com as notificações não lidas.</returns>
        public async Task<IEnumerable<NotificacaoDto>> HandleAsync(ObterNotificacoesPorUsuarioNaoLidasQuery query, CancellationToken ct)
        {
            try
            {
                var notificacoes = await _repository.ListarNaoLidasPorUsuarioAsync(query.UsuarioId, ct);

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