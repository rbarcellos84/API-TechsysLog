using System;
using System.Collections.Generic;
using TechsysLog.Application.Dtos.Notificacoes;
using TechsysLog.Application.Queries.Interfaces;

namespace TechsysLog.Application.Queries.Notificacoes
{
    /// <summary>
    /// Query para obter todas as notificações de um usuário específico.
    /// Retorna uma coleção de objetos <see cref="NotificacaoDto"/>.
    /// </summary>
    public class ObterNotificacoesPorUsuarioQuery : IQuery<IEnumerable<NotificacaoDto>>
    {
        /// <summary>
        /// Identificador único do usuário cujas notificações serão consultadas.
        /// </summary>
        public Guid UsuarioId { get; }

        /// <summary>
        /// Construtor da query de notificações por usuário.
        /// </summary>
        /// <param name="usuarioId">Identificador do usuário.</param>
        public ObterNotificacoesPorUsuarioQuery(Guid usuarioId)
        {
            UsuarioId = usuarioId;
        }
    }
}
