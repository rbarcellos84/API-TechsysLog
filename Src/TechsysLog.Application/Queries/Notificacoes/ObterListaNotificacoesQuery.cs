using System.Collections.Generic;
using TechsysLog.Application.Dtos.Notificacoes;
using TechsysLog.Application.Queries.Interfaces;

namespace TechsysLog.Application.Queries.Notificacoes
{
    /// <summary>
    /// Query para obter a lista de notificações registradas no sistema.
    /// Retorna uma coleção de objetos <see cref="NotificacaoDto"/>.
    /// </summary>
    public class ObterListaNotificacoesQuery : IQuery<IEnumerable<NotificacaoDto>>
    {
    }
}
