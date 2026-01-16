using System;
using System.Collections.Generic;
using TechsysLog.Application.Dtos.Notificacoes;
using TechsysLog.Application.Queries.Interfaces;

namespace TechsysLog.Application.Queries.Notificacoes
{
    /// <summary>
    /// Query para obter a lista de notificações não lidas de um usuário específico.
    /// Retorna uma coleção de objetos <see cref="NotificacaoDto"/>.
    /// </summary>
    public class ObterNotificacoesPorUsuarioNaoLidasQuery : IQuery<IEnumerable<NotificacaoDto>>
    {
        /// <summary>
        /// Identificador único do usuário cujas notificações não lidas serão consultadas.
        /// </summary>
        public Guid UsuarioId { get; }

        /// <summary>
        /// Construtor da query de notificações não lidas por usuário.
        /// </summary>
        /// <param name="usuarioId">Identificador do usuário.</param>
        public ObterNotificacoesPorUsuarioNaoLidasQuery(Guid usuarioId)
        {
            UsuarioId = usuarioId;
        }
    }
}
