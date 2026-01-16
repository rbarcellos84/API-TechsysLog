using AutoMapper;
using TechsysLog.Domain.Entities;
using TechsysLog.Application.Dtos.Notificacoes;

namespace TechsysLog.Application.Mappings
{
    /// <summary>
    /// Profile de mapeamento para a entidade Notificacao.
    /// Responsável apenas por converter de Notificacao para NotificacaoDto.
    /// </summary>
    public class NotificacaoMapping : Profile
    {
        public NotificacaoMapping()
        {
            /// <summary>
            /// Mapeia de Notificacao para NotificacaoDto.
            /// </summary>
            CreateMap<Notificacao, NotificacaoDto>();
        }
    }
}
