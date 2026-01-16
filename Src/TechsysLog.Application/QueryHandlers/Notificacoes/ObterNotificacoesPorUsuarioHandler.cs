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
    /// Handler responsável por processar a query de recuperação de notificações filtradas por um usuário específico.
    /// </summary>
    public class ObterNotificacoesPorUsuarioHandler : IQueryHandler<ObterNotificacoesPorUsuarioQuery, IEnumerable<NotificacaoDto>>
    {
        private readonly INotificacaoRepository _repository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ObterNotificacoesPorUsuarioHandler"/>.
        /// </summary>
        /// <param name="repository">Instância do repositório de notificações injetada via DI.</param>
        public ObterNotificacoesPorUsuarioHandler(INotificacaoRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Executa a consulta para listar as notificações pertencentes ao ID do usuário informado, com tratamento de exceções.
        /// </summary>
        /// <param name="query">Objeto de consulta contendo o identificador do usuário.</param>
        /// <param name="ct">Token de cancelamento para operações assíncronas.</param>
        /// <returns>Uma coleção de <see cref="NotificacaoDto"/> mapeada com as notificações do usuário.</returns>
        public async Task<IEnumerable<NotificacaoDto>> HandleAsync(ObterNotificacoesPorUsuarioQuery query, CancellationToken ct)
        {
            try
            {
                var notificacoes = await _repository.ListarPorUsuarioAsync(query.UsuarioId, ct);

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