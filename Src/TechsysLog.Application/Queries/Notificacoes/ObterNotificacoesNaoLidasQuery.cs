using System.Collections.Generic;
using TechsysLog.Application.Dtos.Notificacoes;
using TechsysLog.Application.Queries.Interfaces;

namespace TechsysLog.Application.Queries.Notificacoes
{
    /// <summary>
    /// Query para obter a lista de notificações que ainda não foram lidas pelo usuário.
    /// Retorna uma coleção de objetos <see cref="NotificacaoDto"/>.
    /// </summary>
    public class ObterNotificacoesNaoLidasQuery : IQuery<IEnumerable<NotificacaoDto>>
    {
    }
}
